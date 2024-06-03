namespace FinalProjectApp.Application.Features.Products.Queries.GetAllProducts
{
    public class ProductGetDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool InStock { get; set; }
        public bool IsDeleted { get; set; }
        public int UnitsInStock { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public SubCategory SubCategory { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<Colour> Colours { get; set; }
        public virtual List<Testimonial> Testimonials { get; set; }
    }
}
