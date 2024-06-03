namespace FinalProjectApp.Application.Features.Services.Commands.UpdateCommand
{
    public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateServiceCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await _context.Services.FindAsync(request.Id, cancellationToken);

            service.Title = request.Title;
            service.Description = request.Description;
            service.LastModifiedBy = "Admin";
            service.LastModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
