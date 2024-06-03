namespace FinalProjectApp.Application.Features.Testimonials.Commands.ApproveComment
{
    public class ApproveCommentCommandHandler : IRequestHandler<ApproveCommentCommand>
    {
        private readonly IApplicationDbContext _context;
        public ApproveCommentCommandHandler(IApplicationDbContext context) => _context = context;
        public async Task Handle(ApproveCommentCommand request, CancellationToken cancellationToken)
        {
            var testimonial = await _context.Testimonials
                .Where(t => t.Id == request.Id && !t.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);

            testimonial.IsApproved = true;
            testimonial.IsChecked = true;
            _context.Testimonials.Update(testimonial);

            await _context.Testimonials.AddAsync(testimonial);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
