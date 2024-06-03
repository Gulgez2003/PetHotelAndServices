namespace FinalProjectApp.Application.Features.Testimonials.Queries.GetAllComments
{
    public class GetCommentQueryHandler : IRequestHandler<GetCommentsQuery, List<TestimonialGetDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCommentQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<TestimonialGetDTO>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
        {
            var testimonials = await _context.Testimonials
                .Where(t => !t.IsDeleted && !t.IsChecked)
                .Include(s => s.Product)
                .OrderBy(s => s.Id)
                .ToListAsync();

            var testimonialDTOs = _mapper.Map<List<TestimonialGetDTO>>(testimonials);

            return testimonialDTOs;
        }
    }
}
