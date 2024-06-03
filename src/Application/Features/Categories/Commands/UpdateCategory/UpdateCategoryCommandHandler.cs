namespace FinalProjectApp.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateCategoryCommandHandler(IApplicationDbContext context) => _context = context;
        
        public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(request.Id, cancellationToken);

            category.Name = request.Name;
            category.LastModifiedBy = "Admin";
            category.LastModifiedDate = DateTime.Now;

            if (request.SelectedSubCategories!= null)
            {
                var selectedSubCategories = await _context.SubCategories.Where(a => request.SelectedSubCategories.Contains(a.Id)).ToListAsync();
                category.SubCategories.AddRange(selectedSubCategories);
            }
            if (request.SelectedAnimals != null)
            {
                var selectedAnimals = await _context.Animals.Where(a => request.SelectedAnimals.Contains(a.Id)).ToListAsync();
                category.Animals.AddRange(selectedAnimals);
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
