namespace FinalProjectApp.Application.Features.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, UserCommandResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginUserCommandHandler(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<UserCommandResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new UserCommandResult();

            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                commandResult.IsValid = false;
                commandResult.CommandErrors.Add(string.Empty, new List<string> { "Email or password is incorrect." });
                return commandResult;
            }

            ApplicationUser user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                {
                    commandResult.IsValid = false;
                    commandResult.CommandErrors.Add(string.Empty, new List<string> { "Email or password is incorrect." });
                    return commandResult;
                }
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

            if (!result.Succeeded)
            {
                commandResult.IsValid = false;
                commandResult.CommandErrors.Add(string.Empty, new List<string> { "Email or password is incorrect." });
                return commandResult;
            }

            return commandResult;
        }
    }
}
