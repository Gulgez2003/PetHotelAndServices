namespace FinalProjectApp.Application.Features.SubCategories.Commands.UpdateSubCategory
{
    public class UpdateSubCategoryCommandHandler : IRequestHandler<UpdateSubCategoryCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateSubCategoryCommandHandler(IApplicationDbContext context) => _context = context;
        public async Task Handle(UpdateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var subCategory = await _context.SubCategories.FindAsync(request.Id, cancellationToken);

            subCategory.Name = request.Name;
            subCategory.CategoryId = request.CategoryId;
            subCategory.LastModifiedBy = "Admin";
            subCategory.LastModifiedDate = DateTime.Now;

            if (request.SelectedProducts != null)
            {
                var selectedProducts = await _context.Products.Where(a => request.SelectedProducts.Contains(a.Id)).ToListAsync();
                subCategory.Products.AddRange(selectedProducts);
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
