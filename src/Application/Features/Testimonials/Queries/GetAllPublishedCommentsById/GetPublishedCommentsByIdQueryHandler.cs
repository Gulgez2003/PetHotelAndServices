namespace FinalProjectApp.Application.Features.Testimonials.Queries.GetPublishedCommentById
{
    public class GetPublishedCommentsByIdQueryHandler : IRequestHandler<GetPublishedCommentsByIdQuery, List<TestimonialGetDTO>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetPublishedCommentsByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<TestimonialGetDTO>> Handle(GetPublishedCommentsByIdQuery request, CancellationToken cancellationToken)
        {
            var testimonials = await _context.Testimonials
                .Where(t => t.ProductId == request.Id && !t.IsDeleted && t.IsApproved)
                .Include(t => t.Product)
                .FirstOrDefaultAsync(cancellationToken);

            var testimonialDTO = _mapper.Map<List<TestimonialGetDTO>>(testimonials);
            return testimonialDTO;
        }
    }
}
