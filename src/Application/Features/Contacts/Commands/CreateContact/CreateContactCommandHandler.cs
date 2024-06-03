namespace FinalProjectApp.Application.Features.Contacts.Commands.CreateContact
{
    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateContactCommandHandler(IApplicationDbContext context) => _context = context;
        
        public async Task Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            Contact contact = new()
            {
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                Message = request.Message
            };

            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
