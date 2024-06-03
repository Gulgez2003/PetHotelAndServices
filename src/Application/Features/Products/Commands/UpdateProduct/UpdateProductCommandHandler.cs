namespace FinalProjectApp.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISender _sender;
        public UpdateProductCommandHandler(IApplicationDbContext context, ISender sender)
        {
            _context = context;
            _sender = sender;
        }

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(request.Id, cancellationToken);

            product.Title = request.Title;
            product.Description = request.Description;
            product.Price = request.Price;
            product.UnitsInStock = request.UnitsInStock;
            product.SubCategoryId = request.SubCategoryId;
            product.LastModifiedBy = "Admin";
            product.LastModifiedDate = DateTime.Now;
            product.ProductImages = await _sender.Send(new CreateProductImageCommand { Files = request.Files, MainExists = true }, cancellationToken);

            if (request.Colours != null)
            {
                foreach (var item in request.Colours)
                {
                    product.Colours.Add(item);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
