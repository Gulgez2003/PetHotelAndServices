namespace FinalProjectApp.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateProductCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Title)
                .MinimumLength(2)
                .MaximumLength(50)
                .NotEmpty()
                .NotNull()
                .MustAsync((command, title, cancellationToken) => BeUniqueTitle(command.Id, title, cancellationToken))
                .WithMessage("Product with the same title already exists");
            RuleFor(p => p.Description)
                 .NotNull()
                 .NotEmpty()
                 .MinimumLength(2);
            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0)
                .LessThan(300);
            RuleFor(p => p.UnitsInStock)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);
        }

        private async Task<bool> BeUniqueTitle(int productId, string title, CancellationToken cancellationToken)
        {
            return await _context.Products
                .Where(a => a.Title == title && a.Id != productId)
                .CountAsync(cancellationToken) == 0;
        }
    }
}
