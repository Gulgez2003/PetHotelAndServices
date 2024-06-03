namespace FinalProjectApp.Application.Features.SubCategories.Commands.CreateSubCategory
{
    public class CreateSubCategoryCommandHandler : IRequestHandler<CreateSubCategoryCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateSubCategoryCommandHandler(IApplicationDbContext context) => _context = context;
        public async Task Handle(CreateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            SubCategory subCategory = new()
            {
                Name = request.Name,
                CategoryId = request.CategoryId,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "Admin"
            };

            if (request.Products != null)
            {
                var selectedProducts = await _context.Products.Where(a => request.SelectedProducts.Contains(a.Id)).ToListAsync();

                foreach (var item in selectedProducts)
                {
                    subCategory.Products.Add(item);
                }
            }

            await _context.SubCategories.AddAsync(subCategory);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
