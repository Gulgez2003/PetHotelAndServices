namespace FinalProjectApp.Application.Features.SubCategories.Queries.GetSubCategoryById
{
    public class GetSubCategoryByIdQueryHandler : IRequestHandler<GetSubCategoryByIdQuery, SubCategoryGetDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetSubCategoryByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SubCategoryGetDTO> Handle(GetSubCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var subCategory = await _context.SubCategories
                .Where(s => s.Id == request.Id && !s.IsDeleted)
                .Include(s => s.Category)
                .FirstOrDefaultAsync(cancellationToken);

            var subCategoryDTO = _mapper.Map<SubCategoryGetDTO>(subCategory);
            return subCategoryDTO;
        }
    }
}
