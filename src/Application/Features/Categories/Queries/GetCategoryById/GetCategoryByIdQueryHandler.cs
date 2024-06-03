namespace FinalProjectApp.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryGetDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetCategoryByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CategoryGetDTO> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .Include(c => c.Animals)
                .Include(c => c.SubCategories)
                .Where(c => c.Id == request.Id && !c.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);

            var categoryDTO = _mapper.Map<CategoryGetDTO>(category);
            return categoryDTO;
        }
    }
}
