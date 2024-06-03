namespace FinalProjectApp.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateCategoryCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50)
                .MustAsync((command, name, cancellationToken) => BeUniqueName(command.Id, name, cancellationToken))
                .WithMessage("Category with the same name already exists");
        }

        private async Task<bool> BeUniqueName(int categoryId, string name, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .Where(a => a.Name == name && a.Id != categoryId)
                .CountAsync(cancellationToken) == 0;
        }
    }
}
