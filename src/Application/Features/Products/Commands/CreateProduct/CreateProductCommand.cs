namespace FinalProjectApp.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int UnitsInStock { get; set; }
        public decimal Price { get; set; }
        public int SubCategoryId { get; set; }
        public List<Colour> Colours { get; set; }
        public List<IFormFile> Files { get; set; }
        public CreateProductCommand()
        {
            Colours = new List<Colour>();
            Files = new List<IFormFile>();
        }
    }
}
