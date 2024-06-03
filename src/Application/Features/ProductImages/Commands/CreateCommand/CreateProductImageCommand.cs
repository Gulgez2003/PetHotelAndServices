namespace FinalProjectApp.Application.Features.ProductImages.Commands.CreateCommand
{
    public class CreateProductImageCommand : IRequest<List<ProductImage>>
    {
        public string Path { get; set; }
        public List<IFormFile> Files { get; set; }
        public bool IsMain { get; set; }
        public bool MainExists { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
