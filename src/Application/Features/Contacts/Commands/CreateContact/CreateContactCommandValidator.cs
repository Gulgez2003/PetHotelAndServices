namespace FinalProjectApp.Application.Features.Contacts.Commands.CreateContact
{
    public class CreateContactCommandValidator : AbstractValidator<CreateContactCommand>
    {
        public CreateContactCommandValidator()
        {
            RuleFor(c => c.FullName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
            RuleFor(c => c.PhoneNumber)
                .NotNull()
                .NotEmpty()
                .MinimumLength(7)
                .MaximumLength(15);
            RuleFor(c => c.Message)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2);
            RuleFor(c => c.Email)
                .NotNull()
                .NotEmpty()
                .MaximumLength(128)
                .EmailAddress()
                .MinimumLength(5);
        }
    }
}
