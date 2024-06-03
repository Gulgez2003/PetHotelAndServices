namespace FinalProjectApp.Application.Features.Galleries.Commands.CreateGallery
{
    public class CreateGalleryCommand : IRequest
    {
        public List<IFormFile>? Files { get; set; }
    }
}
