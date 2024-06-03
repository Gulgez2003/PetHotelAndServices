namespace FinalProjectApp.Application.Features.SubCategories.Commands.DeleteSubCategory
{
    public class DeleteSubCategoryCommandHandler : IRequestHandler<DeleteSubCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteSubCategoryCommandHandler(IApplicationDbContext context) => _context = context;

        public async Task Handle(DeleteSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var subCategory = await _context.SubCategories
                 .Where(a => a.Id == request.Id && !a.IsDeleted)
                 .FirstOrDefaultAsync(cancellationToken);

            _context.SubCategories.Remove(subCategory);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
