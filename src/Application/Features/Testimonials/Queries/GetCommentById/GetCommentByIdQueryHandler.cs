using FinalProjectApp.Application.Features.Testimonials.Queries.GetPublishedCommentById;

namespace FinalProjectApp.Application.Features.Testimonials.Queries.GetCommentById
{
    public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, TestimonialGetDTO>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetCommentByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TestimonialGetDTO> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
        {
            var testimonial = await _context.Testimonials
                .Where(t => t.Id == request.Id && !t.IsDeleted)
                .Include(t => t.Product)
                .FirstOrDefaultAsync(cancellationToken);

            var testimonialDTO = _mapper.Map<TestimonialGetDTO>(testimonial);
            return testimonialDTO;
        }
    }
}
