namespace FinalProjectApp.Application.Features.Services.Commands.DeleteCommand
{
    public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteServiceCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await _context.Services
                 .Where(a => a.Id == request.Id && !a.IsDeleted)
                 .FirstOrDefaultAsync(cancellationToken);

            _context.Services.Remove(service);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
