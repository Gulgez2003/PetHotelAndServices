namespace FinalProjectApp.Application.Features.Categories.Queries.GetAllCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryGetDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCategoriesQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<CategoryGetDTO>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _context.Categories
               .OrderBy(c => c.Id)
               .ToListAsync();

            var categoryDTOs = _mapper.Map<List<CategoryGetDTO>>(categories);

            return categoryDTOs;
        }
    }
}
