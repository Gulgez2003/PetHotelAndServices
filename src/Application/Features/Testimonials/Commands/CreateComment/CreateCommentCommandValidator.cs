namespace FinalProjectApp.Application.Features.Testimonials.Commands.CreateComment
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(t => t.Comment)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2);
        }
    }
}
