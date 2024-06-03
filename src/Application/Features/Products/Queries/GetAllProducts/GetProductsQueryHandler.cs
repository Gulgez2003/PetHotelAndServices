namespace FinalProjectApp.Application.Features.Products.Queries.GetAllProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductGetDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProductsQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<ProductGetDTO>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Products
                .Where(a => !a.IsDeleted)
                .Include(p => p.SubCategory)
                .Include(p => p.ProductImages)
                .OrderBy(p => p.Id)
                .ToListAsync();
         
            var productDTOs = _mapper.Map<List<ProductGetDTO>>(products);

            return productDTOs;
        }
    }
}
