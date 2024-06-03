namespace FinalProjectApp.Presentation.ViewModels
{
    public class ProductsViewModel
    {
        public List<ProductGetDTO> Products { get; set; }
        public ProductGetDTO Product { get; set; }
        public CreateCommentCommand CreateCommentCommand { get; set; }
        public List<TestimonialGetDTO> Testimonials { get; set; }
    }
}
