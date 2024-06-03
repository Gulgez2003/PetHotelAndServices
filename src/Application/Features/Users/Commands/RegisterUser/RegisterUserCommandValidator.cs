namespace FinalProjectApp.Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(r => r.FullName)
               .NotNull()
               .NotEmpty()
               .MinimumLength(2)
               .MaximumLength(50);
            RuleFor(r => r.UserName)
               .NotNull()
               .NotEmpty()
               .MinimumLength(2)
               .MaximumLength(50);
            RuleFor(r => r.Address)
              .NotNull().WithMessage("Please provide your delivery address, as it cannot be empty.")
              .NotEmpty().WithMessage("Please provide your delivery address, as it cannot be empty.")
              .MinimumLength(2);
            RuleFor(r => r.PhoneNumber)
               .NotNull()
               .NotEmpty()
               .MinimumLength(7)
               .MaximumLength(15);
            RuleFor(r => r.Email)
                .NotNull()
                .NotEmpty()
                .MaximumLength(128)
                .EmailAddress()
                .MinimumLength(5);
            RuleFor(r => r.Password)
                .NotNull()
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(20);
            RuleFor(r => r.ConfirmPassword)
                .NotNull()
                .NotEmpty()
                .Equal(r => r.Password) 
                .WithMessage("The password and confirmation password do not match. Please make sure you enter the same password in both fields.");
        }
    }
}
