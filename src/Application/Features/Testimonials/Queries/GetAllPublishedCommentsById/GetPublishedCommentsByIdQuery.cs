namespace FinalProjectApp.Application.Features.Testimonials.Queries.GetPublishedCommentById
{
    public class GetPublishedCommentsByIdQuery : IRequest<List<TestimonialGetDTO>>
    {
        public int Id { get; set; }
    }
}
