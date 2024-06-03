namespace FinalProjectApp.Application.Features.Animals.Commands.UpdateAnimal
{
    public class UpdateAnimalCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile? File { get; set; }
        public AnimalImage? AnimalImage { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int[] SelectedCategories { get; set; }
        public virtual List<Category> Categories { get; set; }

        public UpdateAnimalCommand()
        {
            Categories = new List<Category>();
        }
    }
}
