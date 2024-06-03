namespace FinalProjectApp.Application.Features.SubCategories.Commands.UpdateSubCategory
{
    public class UpdateSubCategoryCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int[] SelectedProducts { get; set; }
        public virtual List<Product>? Products { get; set; }

        public UpdateSubCategoryCommand()
        {
            Products = new List<Product>();
        }
    }
}
