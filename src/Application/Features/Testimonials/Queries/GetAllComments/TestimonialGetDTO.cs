namespace FinalProjectApp.Application.Features.Testimonials.Queries.GetAllComments
{
    public class TestimonialGetDTO
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Comment { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsApproved { get; set; }
        public bool IsChecked { get; set; }
        public Product Product { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
