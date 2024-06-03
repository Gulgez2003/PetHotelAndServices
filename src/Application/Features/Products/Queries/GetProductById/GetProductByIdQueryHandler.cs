namespace FinalProjectApp.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductGetDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetProductByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductGetDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Where(p => p.Id == request.Id && !p.IsDeleted)
                .Include(p => p.SubCategory)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(cancellationToken);

            var productDTO = _mapper.Map<ProductGetDTO>(product);
            return productDTO;
        }
    }
}
