namespace FinalProjectApp.Application.Features.Testimonials.Commands.DisapproveComment
{
    public class DisapproveCommentCommandHandler : IRequestHandler<DisapproveCommentCommand>
    {
        private readonly IApplicationDbContext _context;
        public DisapproveCommentCommandHandler(IApplicationDbContext context) => _context = context;
        public async Task Handle(DisapproveCommentCommand request, CancellationToken cancellationToken)
        {
            var testimonial = await _context.Testimonials
                .Where(t => t.Id == request.Id && !t.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);

            testimonial.IsApproved = false;
            testimonial.IsChecked = true;
            _context.Testimonials.Update(testimonial);

            await _context.Testimonials.AddAsync(testimonial);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
