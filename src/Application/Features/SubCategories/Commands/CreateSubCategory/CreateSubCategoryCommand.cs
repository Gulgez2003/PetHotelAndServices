namespace FinalProjectApp.Application.Features.SubCategories.Commands.CreateSubCategory
{
    public class CreateSubCategoryCommand : IRequest
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int[] SelectedProducts { get; set; }
        public virtual List<Product>? Products { get; set; }

        public CreateSubCategoryCommand()
        {
            Products = new List<Product>();
        }
    }
}
