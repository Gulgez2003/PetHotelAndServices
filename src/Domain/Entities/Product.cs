namespace Domain.Entities
{
    public class Product : BaseAuditableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool InStock { get; set; }
        public int UnitsInStock {  get; set; }
        public decimal Price {  get; set; }
        public SubCategory SubCategory { get; set; }
        public int SubCategoryId { get; set; }
        public List<Colour>? Colours { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<Testimonial> Testimonials { get; set; }
        public Product()
        {
            Colours = new List<Colour>();
            ProductImages = new List<ProductImage>();
            Testimonials = new List<Testimonial>();
        }
    }
}
