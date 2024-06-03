namespace FinalProjectApp.Presentation.ViewModels
{
    public class CartItemViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public bool InStock { get; set; }
        public List<Colour> Colours { get; set; }
        public int ProductCount { get; set; }
        public CartItemViewModel()
        {
            ProductImages = new List<ProductImage>();
        }
    }
}
