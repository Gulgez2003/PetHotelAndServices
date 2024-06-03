namespace FinalProjectApp.Application.Features.Services.Commands.CreateCommand
{
    public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateServiceCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(c => c.Title)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50)
                .MustAsync(BeUniqueTitle).WithMessage("Service with the same title already exists");
            RuleFor(c => c.Description)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(200);
        }

        private async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
        {
            return !(await _context.Services.AnyAsync(p => p.Title == title, cancellationToken));
        }
    }
}
