namespace FinalProjectApp.Application.Features.Services.Commands.CreateCommand
{
    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateServiceCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {
            Service service = new()
            {
                Title = request.Title,
                Description = request.Description,
                CreatedDate = DateTime.Now,
                CreatedBy = "Admin"
            };

            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
