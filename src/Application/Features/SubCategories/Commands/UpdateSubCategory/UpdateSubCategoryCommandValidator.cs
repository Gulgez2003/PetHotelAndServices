namespace FinalProjectApp.Application.Features.SubCategories.Commands.UpdateSubCategory
{
    public class UpdateSubCategoryCommandValidator : AbstractValidator<UpdateSubCategoryCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateSubCategoryCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(a => a.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50)
                .MustAsync((command, name, cancellationToken) => BeUniqueName(command.Id, name, cancellationToken))
                .WithMessage("Sub-category with the same name already exists");
        }

        private async Task<bool> BeUniqueName(int subCategoryId, string name, CancellationToken cancellationToken)
        {
            return await _context.SubCategories
                .Where(a => a.Name == name && a.Id != subCategoryId)
                .CountAsync(cancellationToken) == 0;
        }
    }
}
