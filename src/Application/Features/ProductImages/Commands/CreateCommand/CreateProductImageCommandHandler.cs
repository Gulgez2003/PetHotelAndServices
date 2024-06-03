namespace FinalProjectApp.Application.Features.ProductImages.Commands.CreateCommand
{
    public class CreateProductImageCommandHandler : IRequestHandler<CreateProductImageCommand, List<ProductImage>>
    {
        private readonly IHostingEnvironment _env;

        public CreateProductImageCommandHandler(IHostingEnvironment env)
        {
            _env = env;
        }

        public async Task<List<ProductImage>> Handle(CreateProductImageCommand request, CancellationToken cancellationToken)
        {
            List<ProductImage> Images = new();
            foreach (var file in request.Files)
            {
                string ImageName = FileExtension.CreateFile(file, _env.WebRootPath, "assets/img/post");
                ProductImage newImage = new()
                {
                    Path = ImageName,
                    IsMain = false
                };
                if (request.MainExists && request.Files.IndexOf(file) is 0)
                    newImage.IsMain = true;
                Images.Add(newImage);
            }
            return Images;
        }
    }
}
