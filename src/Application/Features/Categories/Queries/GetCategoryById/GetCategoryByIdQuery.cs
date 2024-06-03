namespace FinalProjectApp.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<CategoryGetDTO>
    {
        public int Id { get; set; }
    }
}
