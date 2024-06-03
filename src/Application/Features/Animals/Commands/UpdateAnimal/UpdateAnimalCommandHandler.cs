namespace FinalProjectApp.Application.Features.Animals.Commands.UpdateAnimal
{
    public class UpdateAnimalCommandHandler : IRequestHandler<UpdateAnimalCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISender _sender;
        public UpdateAnimalCommandHandler(IApplicationDbContext context, ISender sender)
        {
            _context = context;
            _sender = sender;
        }
        public async Task Handle(UpdateAnimalCommand request, CancellationToken cancellationToken)
        {
            var animal = await _context.Animals
                .Where(a => a.Id == request.Id && !a.IsDeleted)
                .FirstOrDefaultAsync(cancellationToken);

            animal.Name = request.Name;
            animal.LastModifiedBy = "Admin";
            animal.LastModifiedDate = DateTime.Now;

            if (request.SelectedCategories != null)
            {
                var selectedCategories = await _context.Categories.Where(a => request.SelectedCategories.Contains(a.Id)).ToListAsync();
                animal.Categories.AddRange(selectedCategories);
            }

            if (request.File != null)
            {
                animal.AnimalImage = await _sender.Send(new CreateAnimalImageCommand { File = request.File }, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
