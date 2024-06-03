namespace FinalProjectApp.Application.Features.Animals.Commands.CreateAnimal
{
    public class CreateAnimalCommandHandler : IRequestHandler<CreateAnimalCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISender _sender;
        public CreateAnimalCommandHandler(IApplicationDbContext context, ISender sender)
        {
            _context = context;
            _sender = sender;
        }

        public async Task Handle(CreateAnimalCommand request, CancellationToken cancellationToken)
        {
            Animal animal = new()
            {
                Name = request.Name,
                CreatedDate = DateTime.Now,
                CreatedBy = "Admin",
                AnimalImage = await _sender.Send(new CreateAnimalImageCommand { File = request.File }, cancellationToken)
            };

            if (request.SelectedCategories != null)
            {
                var selectedCategories = await _context.Categories.Where(a => request.SelectedCategories.Contains(a.Id)).ToListAsync();

                foreach (var item in selectedCategories)
                {
                    animal.Categories.Add(item);
                }
            }

            await _context.Animals.AddAsync(animal);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
