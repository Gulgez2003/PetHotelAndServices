namespace FinalProjectApp.Application.Features.Animals.Commands.DeleteAnimal
{
    public class DeleteAnimalCommand : IRequest
    {
        public int Id { get; set; }
    }
}
