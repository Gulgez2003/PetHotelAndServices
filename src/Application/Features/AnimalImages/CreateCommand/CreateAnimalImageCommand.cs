namespace FinalProjectApp.Application.Features.AnimalImages.CreateCommand
{
    public class CreateAnimalImageCommand : IRequest<AnimalImage>
    {
        public string Path { get; set; }
        public IFormFile File { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
    }
}
