namespace FinalProjectApp.Application.Features.SubCategories.Queries.GetAllSubCategories
{
    public class GetSubCategoriesQueryHandler : IRequestHandler<GetSubCategoriesQuery, List<SubCategoryGetDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSubCategoriesQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<SubCategoryGetDTO>> Handle(GetSubCategoriesQuery request, CancellationToken cancellationToken)
        {
            var subCategories = await _context.SubCategories
                .Include(s => s.Category)
               .OrderBy(s => s.Id)
               .ToListAsync();

            var subCategoryDTOs = _mapper.Map<List<SubCategoryGetDTO>>(subCategories);

            return subCategoryDTOs;
        }
    }
}
