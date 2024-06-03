namespace FinalProjectApp.Application.Features.Animals.Commands.UpdateAnimal
{
    public class UpdateAnimalCommandValidator : AbstractValidator<UpdateAnimalCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateAnimalCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .MinimumLength(2)
                .MaximumLength(50)
                .NotEmpty()
                .NotNull()
                .MustAsync((command, name, cancellationToken) => BeUniqueName(command.Id, name, cancellationToken))
                .WithMessage("Animal with the same name already exists");
        }

        private async Task<bool> BeUniqueName(int animalId, string name, CancellationToken cancellationToken)
        {
            return await _context.Animals
                .Where(a => a.Name == name && a.Id != animalId)
                .CountAsync(cancellationToken) == 0;
        }
    }
}
