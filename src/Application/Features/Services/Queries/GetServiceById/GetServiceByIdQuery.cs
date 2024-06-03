namespace FinalProjectApp.Application.Features.Services.Queries.GetServiceById
{
    public class GetServiceByIdQuery : IRequest<ServiceGetDTO>
    {
        public int Id { get; set; }
    }
}
