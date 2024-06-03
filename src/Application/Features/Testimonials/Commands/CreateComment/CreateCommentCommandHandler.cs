namespace FinalProjectApp.Application.Features.Testimonials.Commands.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateCommentCommandHandler(IApplicationDbContext context) => _context = context;
        public async Task Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            Testimonial testimonial = new()
            {
                Comment = request.Comment,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = "User"
            };

            await _context.Testimonials.AddAsync(testimonial);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
