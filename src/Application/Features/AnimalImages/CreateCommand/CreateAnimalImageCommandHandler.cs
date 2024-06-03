namespace FinalProjectApp.Application.Features.AnimalImages.CreateCommand
{
    public class CreateAnimalImageCommandHandler : IRequestHandler<CreateAnimalImageCommand, AnimalImage>
    {
        private readonly IHostingEnvironment _env;

        public CreateAnimalImageCommandHandler(IHostingEnvironment env)
        {
            _env = env;
        }

        public async Task<AnimalImage> Handle(CreateAnimalImageCommand request, CancellationToken cancellationToken)
        {
            string ImageName = FileExtension.CreateFile(request.File, _env.WebRootPath, "assets/img/post");
            AnimalImage newImage = new()
            {
                Path = ImageName
            };

            return newImage;
        }
    }
}

