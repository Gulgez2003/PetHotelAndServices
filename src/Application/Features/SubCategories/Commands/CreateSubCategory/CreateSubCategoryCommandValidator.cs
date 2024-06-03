namespace FinalProjectApp.Application.Features.SubCategories.Commands.CreateSubCategory
{
    public class CreateSubCategoryCommandValidator : AbstractValidator<CreateSubCategoryCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateSubCategoryCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(a => a.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50)
                .MustAsync(BeUniqueName).WithMessage("Sub-category with the same name already exists");
            //RuleFor(v => v.SelectedProducts)
            //    .NotNull()
            //    .NotEmpty();
        }

        private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
        {
            return !(await _context.SubCategories.AnyAsync(p => p.Name == name, cancellationToken));
        }
    }
}
