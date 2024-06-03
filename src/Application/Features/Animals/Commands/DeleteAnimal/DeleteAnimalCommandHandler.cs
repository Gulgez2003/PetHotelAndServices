namespace FinalProjectApp.Application.Features.Animals.Commands.DeleteAnimal
{
    public class DeleteAnimalCommandHandler : IRequestHandler<DeleteAnimalCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteAnimalCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task Handle(DeleteAnimalCommand request, CancellationToken cancellationToken)
        {
            var animal = await _context.Animals
                .Where(a => a.Id == request.Id && !a.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);

            _context.Animals.Remove(animal);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
