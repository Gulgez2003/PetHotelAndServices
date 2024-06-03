namespace FinalProjectApp.Application.Features.Services.Commands.UpdateCommand
{
    public class UpdateServiceCommand : IRequest
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
