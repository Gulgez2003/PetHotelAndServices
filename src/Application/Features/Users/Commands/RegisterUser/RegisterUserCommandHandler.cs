namespace FinalProjectApp.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserCommandResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserCommandResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var commandResult = new UserCommandResult();

            ApplicationUser user = new ApplicationUser()
            {
                FullName = request.FullName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                Email = request.Email
            };

            IdentityResult result = new IdentityResult();

            if (request.Password != null)
            {
                if (request.Password == request.ConfirmPassword)
                {
                    result = await _userManager.CreateAsync(user, request.Password);

                    if (!result.Succeeded)
                    {
                        commandResult.IsValid = false;
                        foreach (var error in result.Errors)
                        {
                            var propertyName = "";
                            if (error.Code.Contains("UserName"))
                            {
                                propertyName = "UserName";
                            }
                            else if (error.Code.Contains("Email"))
                            {
                                propertyName = "Email";
                            }
                            else if (error.Code.Contains("Password"))
                            {
                                propertyName = "Password";
                            }

                            if (!commandResult.CommandErrors.ContainsKey(propertyName))
                            {
                                commandResult.CommandErrors[propertyName] = new List<string>();
                            }
                            commandResult.CommandErrors[propertyName].Add(error.Description);
                        }
                        return commandResult;
                    }
                }
                else
                {
                    commandResult.IsValid = false;
                    commandResult.CommandErrors["Password"] = new List<string> { "The password and confirmation password do not match. Please make sure you enter the same password in both fields." };
                    return commandResult;
                }
            }
            else
            {
                commandResult.IsValid = false;
                commandResult.CommandErrors["Password"] = new List<string> { "Password cannot be null." };
                return commandResult;

            }

            await _userManager.AddToRoleAsync(user, "Admin");
            return commandResult;
        }
    }
}