namespace FinalProjectApp.Application.Features.SubCategories.Queries.GetSubCategoryById
{
    public class GetSubCategoryByIdQuery : IRequest<SubCategoryGetDTO>
    {
        public int Id { get; set; }
    }
}
