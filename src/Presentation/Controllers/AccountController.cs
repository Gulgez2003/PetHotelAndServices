using SendGrid.Helpers.Mail;
using SendGrid;

namespace FinalProjectApp.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ISender _sender;
        public AccountController(RoleManager<IdentityRole> roleManager, ISender sender, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _roleManager = roleManager;
            _sender = sender;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Create()
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
            await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
            await _roleManager.CreateAsync(new IdentityRole { Name = "User" });
            return Json("Ok");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            var commandResult = await _sender.Send(command);
            if (!commandResult.IsValid)
            {
                foreach (var error in commandResult.CommandErrors)
                {
                    foreach (var errorMessage in error.Value)
                    {
                        ModelState.AddModelError(error.Key, errorMessage);
                    }
                }
                return View(command);
            }
            return RedirectToAction("Login", "Account");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        // POST: AccountController/ForgetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ResetPasswordCommand command)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(command.Email);
            if (user is null)
            {
                return NotFound();
            }

            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string? link = Url.Action(nameof(ResetPassword), "Account", new { email = user.Email, token = token }, Request.Scheme, Request.Host.ToString());

            var apiKeyValue = "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";
            Environment.SetEnvironmentVariable("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", apiKeyValue);

            var apiKey = Environment.GetEnvironmentVariable("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");

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                Console.WriteLine("SendGrid API key is missing or invalid.");
            }
            else
            {
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("zoosfera7@gmail.com", "ZooSfera");
                var subject = "Please, use the link to reset your password";
                var to = new EmailAddress(user.Email, "Example User");
                var plainTextContent = "Please, use the link to reset your password";
                var htmlContent = link;

                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

                var response = await client.SendEmailAsync(msg);

                if (response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    Console.WriteLine("Email sent successfully.");
                }
                else
                {
                    Console.WriteLine($"Error sending email. Status code: {response.StatusCode}");

                }
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ResetPassword(string email, string token)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return NotFound();
            }

            ResetPasswordCommand command = new ResetPasswordCommand()
            {
                Email = email,
                Token = token
            };

            return View(command);
        }

        // POST: AccountController/ResetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(command.Email);
            if (user is null)
            {
                return NotFound();
            }

            IdentityResult result = new IdentityResult();

            if (command.Password != null)
            {
                result = await _userManager.ResetPasswordAsync(user, command.Token, command.Password);

                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View(command);
                }
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }

            return RedirectToAction("Login", "Account");
        }

        public ActionResult Login()
        {
            return View();
        }

        // POST: AccountController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            var commandResult = await _sender.Send(command);
            if (!commandResult.IsValid)
            {
                foreach (var error in commandResult.CommandErrors)
                {
                    foreach (var errorMessage in error.Value)
                    {
                        ModelState.AddModelError(error.Key, errorMessage);
                    }
                }
                return View(command);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
