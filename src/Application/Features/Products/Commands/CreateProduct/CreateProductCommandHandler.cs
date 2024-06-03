namespace FinalProjectApp.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISender _sender;
        public CreateProductCommandHandler(IApplicationDbContext context, ISender sender)
        {
            _context = context;
            _sender = sender;
        }
        public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            bool inStock = request.UnitsInStock > 0;
            Product product = new()
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                UnitsInStock = request.UnitsInStock,
                InStock = inStock,
                SubCategoryId = request.SubCategoryId,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "Admin",
                ProductImages = await _sender.Send(new CreateProductImageCommand { Files = request.Files, MainExists = true }, cancellationToken)
            };

            if (request.Colours != null)
            {
                foreach (var item in request.Colours)
                {
                    product.Colours.Add(item);
                }
            }

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
