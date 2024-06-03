namespace Domain.Entities
{
    public class Testimonial : BaseAuditableEntity
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Comment { get; set; }
        public bool IsApproved { get; set; }
        public bool IsChecked { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
