namespace FinalProjectApp.Application.Features.Animals.Commands.CreateAnimal
{
    public class CreateAnimalCommandValidator : AbstractValidator<CreateAnimalCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateAnimalCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Name)
                .MinimumLength(2)
                .MaximumLength(50)
                .NotEmpty()
                .NotNull()
                .MustAsync(BeUniqueName).WithMessage("Animal with the same name already exists");
            RuleFor(v => v.File)
                .NotNull().WithMessage("Image must not be null")
                .NotEmpty().WithMessage("Image must not be empty")
                .Must(file => file.IsImage()).WithMessage("File is not an image")
                .Must(file => file.IsSizeOk(5)).WithMessage($"File size exceeds 5 MB");
            RuleFor(v => v.SelectedCategories)
                .NotEmpty()
                .NotNull();
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return !(await _context.Animals.AnyAsync(p => p.Name == name && !p.IsDeleted, cancellationToken));
        }
    }
}
