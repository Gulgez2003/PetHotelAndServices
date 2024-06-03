namespace FinalProjectApp.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateCategoryCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            Category category = new()
            {
                Name = request.Name,
                CreatedDate = DateTime.Now,
                CreatedBy = "Admin"
            };

            if (request.SelectedSubCategories != null)
            {
                var selectedSubCategories = await _context.SubCategories.Where(a => request.SelectedSubCategories.Contains(a.Id)).ToListAsync();

                foreach (var item in selectedSubCategories)
                {
                    category.SubCategories.Add(item);
                }
            }
            if (request.SelectedAnimals != null)
            {
                var selectedAnimals = await _context.Animals.Where(a => request.SelectedAnimals.Contains(a.Id)).ToListAsync();

                foreach (var item in selectedAnimals)
                {
                    category.Animals.Add(item);
                }
            }

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
