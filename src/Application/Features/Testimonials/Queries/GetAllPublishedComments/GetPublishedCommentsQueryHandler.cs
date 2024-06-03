namespace FinalProjectApp.Application.Features.Testimonials.Queries.GetAllPublishedComments
{
    public class GetPublishedCommentsQueryHandler : IRequestHandler<GetPublishedCommentsQuery, List<TestimonialGetDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPublishedCommentsQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<TestimonialGetDTO>> Handle(GetPublishedCommentsQuery request, CancellationToken cancellationToken)
        {
            var testimonials = await _context.Testimonials
                .Where(t => !t.IsDeleted && t.IsApproved)
                .Include(s => s.Product)
                .OrderBy(s => s.Id)
                .ToListAsync();

            var testimonialDTOs = _mapper.Map<List<TestimonialGetDTO>>(testimonials);

            return testimonialDTOs;
        }
    }
}
