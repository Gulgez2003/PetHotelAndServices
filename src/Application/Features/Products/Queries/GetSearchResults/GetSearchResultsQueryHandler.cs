namespace FinalProjectApp.Application.Features.Products.Queries.GetSearchResults
{
    public class GetSearchResultsQueryHandler : IRequestHandler<GetSearchResultsQuery, List<ProductGetDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSearchResultsQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<ProductGetDTO>> Handle(GetSearchResultsQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Products
                .Where(p => !p.IsDeleted && p.Title.Contains(request.Name)
                || p.Description.Contains(request.Name)
                || p.SubCategory.Name.Contains(request.Name)
                )
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
