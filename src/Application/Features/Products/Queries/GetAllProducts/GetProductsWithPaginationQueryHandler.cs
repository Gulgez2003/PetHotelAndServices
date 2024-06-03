using FinalProjectApp.Application.Common.Models;

namespace FinalProjectApp.Application.Features.Products.Queries.GetAllProducts
{
    public class GetProductsWithPaginationQueryHandler : IRequestHandler<GetProductsWithPaginationQuery, List<ProductGetDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetProductsWithPaginationQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<ProductGetDTO>> Handle(GetProductsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Products
                .Where(a => !a.IsDeleted)
                .Include(p => p.SubCategory)
                .Include(p => p.ProductImages)
                .OrderBy(p => p.Id)
                .ToListAsync();

            products = products.Skip((request.CurrentPage - 1) * request.Take).Take(request.Take).ToList();
            var productDTOs = _mapper.Map<List<ProductGetDTO>>(products);

            return productDTOs;
        }
    }
}
