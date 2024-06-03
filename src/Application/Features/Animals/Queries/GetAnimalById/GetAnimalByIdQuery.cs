namespace FinalProjectApp.Application.Features.Animals.Queries.GetAnimalById
{
    public class GetAnimalByIdQuery : IRequest<AnimalGetDTO>
    {
        public int Id { get; set; }
    }
}
