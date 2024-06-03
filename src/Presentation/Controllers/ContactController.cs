using FinalProjectApp.Application.Features.Contacts.Commands.CreateContact;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace Presentation.Controllers
{
    public class ContactController : Controller
    {
        private readonly ISender _sender;
        private readonly ILayoutService _service;
        public ContactController(ISender sender, ILayoutService service)
        {
            _sender = sender;
            _service = service;
        }

        public async Task<IActionResult> Index(CreateContactCommand createCommand)
        {
            SettingGetDTO setting = _service.GetSetting();
            SettingContactViewModel settingContactViewModel = new SettingContactViewModel()
            {
                SettingGetDTO = setting,
                CreateContactCommand = createCommand
            };

            return View(settingContactViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(SettingContactViewModel settingContactViewModel)
        {
            CreateContactCommandValidator validations = new CreateContactCommandValidator();
            ValidationResult validationResult = await validations.ValidateAsync(settingContactViewModel.CreateContactCommand);
            if (validationResult.IsValid)
            {
                await _sender.Send(settingContactViewModel.CreateContactCommand);

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
                    var from = new EmailAddress(settingContactViewModel.CreateContactCommand.Email);
                    var subject = "Please, use the link to reset your password";
                    var to = new EmailAddress("zoosfera7@gmail.com");
                    var plainTextContent = "Contact Form";
                    var htmlContent = $"<p>FullName:  {settingContactViewModel.CreateContactCommand.FullName} </p>" +
                    $"<p>Email:  {settingContactViewModel.CreateContactCommand.Email} </p>" +
                    $"<p>Phone Number:  {settingContactViewModel.CreateContactCommand.PhoneNumber} </p>" +
                    $"<p>Message:  {settingContactViewModel.CreateContactCommand.Message} </p>";

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

                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
            }
            return View(settingContactViewModel);
        }
    }
}
