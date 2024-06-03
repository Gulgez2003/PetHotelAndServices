namespace FinalProjectApp.Application.Features.Services.Commands.CreateCommand
{
    public class CreateServiceCommand : IRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
