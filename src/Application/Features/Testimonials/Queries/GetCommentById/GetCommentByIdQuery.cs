namespace FinalProjectApp.Application.Features.Testimonials.Queries.GetCommentById
{
    public class GetCommentByIdQuery : IRequest<TestimonialGetDTO>
    {
        public int Id { get; set; }
    }
}
