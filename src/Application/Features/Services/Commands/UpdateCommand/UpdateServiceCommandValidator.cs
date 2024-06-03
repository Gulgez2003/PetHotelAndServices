namespace FinalProjectApp.Application.Features.Services.Commands.UpdateCommand
{
    public class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateServiceCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(c => c.Title)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50)
                .MustAsync((command, name, cancellationToken) => BeUniqueTitle(command.Id, name, cancellationToken))
                .WithMessage("Service with the same title already exists");
            RuleFor(c => c.Description)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(150);
        }

        private async Task<bool> BeUniqueTitle(int serviceId, string title, CancellationToken cancellationToken)
        {
            return await _context.Services
                .Where(a => a.Title == title && a.Id != serviceId)
                .CountAsync(cancellationToken) == 0;
        }
    }
}
