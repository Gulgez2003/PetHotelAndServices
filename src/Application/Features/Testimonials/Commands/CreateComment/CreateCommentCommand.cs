namespace FinalProjectApp.Application.Features.Testimonials.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Comment { get; set; }
        public bool IsApproved { get; set; }
        public bool IsChecked { get; set; }
        public int ProductId { get; set; }
    }
}
