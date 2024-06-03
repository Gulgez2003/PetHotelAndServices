namespace FinalProjectApp.Application.Features.Users.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<UserCommandResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
