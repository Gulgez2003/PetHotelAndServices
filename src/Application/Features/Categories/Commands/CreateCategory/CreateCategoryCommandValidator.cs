namespace FinalProjectApp.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateCategoryCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50)
                .MustAsync(BeUniqueName).WithMessage("Category with the same name already exists");
            RuleFor(v => v.SelectedAnimals)
                .NotEmpty()
                .NotNull();
            RuleFor(v => v.SelectedSubCategories)
                .NotEmpty()
                .NotNull();
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return !(await _context.Categories.AnyAsync(p => p.Name == name, cancellationToken));
        }
    }
}
