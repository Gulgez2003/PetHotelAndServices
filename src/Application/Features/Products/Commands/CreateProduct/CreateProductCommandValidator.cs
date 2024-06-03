namespace FinalProjectApp.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateProductCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Title)
                .MinimumLength(2)
                .MaximumLength(50)
                .NotEmpty()
                .NotNull()
                .MustAsync(BeUniqueTitle).WithMessage("Product with the same title already exists");
            RuleFor(p => p.Description)
                 .NotNull()
                 .NotEmpty()
                 .MinimumLength(2);
            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0)
                .LessThan(500);
            RuleFor(p => p.UnitsInStock)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);
            RuleFor(p => p.Files)
               .NotNull()
               .Must(files => files != null && files.Any()).WithMessage("Files must not be empty");

            RuleForEach(p => p.Files)
                .Must(file => file != null && file.Length > 0).WithMessage("File must not be empty")
                .Must(file => file.IsImage()).WithMessage("One or more files are not images")
                .Must(file => file.IsSizeOk(5)).WithMessage($"One or more files size exceeds 5 MB");
        }

        private async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
        {
            return !(await _context.Products.AnyAsync(p => p.Title == title, cancellationToken));
        }
    }
}
