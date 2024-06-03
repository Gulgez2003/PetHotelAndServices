namespace FinalProjectApp.Presentation.ViewModels
{
    public class WishlistItemViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool InStock { get; set; }
        public decimal Price { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}
