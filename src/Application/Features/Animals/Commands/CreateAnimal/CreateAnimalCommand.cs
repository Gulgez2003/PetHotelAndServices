namespace FinalProjectApp.Application.Features.Animals.Commands.CreateAnimal
{
    public class CreateAnimalCommand : IRequest
    {
        public string Name { get; set; }
        public IFormFile? File { get; set; }
        public int[] SelectedCategories { get; set; }
        public virtual List<Category> Categories { get; set; }

        public CreateAnimalCommand()
        {
            Categories = new List<Category>();
        }
    }
}
