namespace FinalProjectApp.Application.Features.Users.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest<UserCommandResult>
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
