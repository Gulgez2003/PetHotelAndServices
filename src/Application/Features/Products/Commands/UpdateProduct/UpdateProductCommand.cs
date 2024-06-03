namespace FinalProjectApp.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UnitsInStock { get; set; }
        public decimal Price { get; set; }
        public int SubCategoryId { get; set; }
        public List<Colour> Colours { get; set; }
        public List<IFormFile> Files { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public UpdateProductCommand()
        {
            Colours = new List<Colour>();
            Files = new List<IFormFile>();
        }
    }
}
