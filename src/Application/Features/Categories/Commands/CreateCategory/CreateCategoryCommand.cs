namespace FinalProjectApp.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest
    {
        public string? Name { get; set; }
        public int[] SelectedSubCategories { get; set; }
        public int[] SelectedAnimals { get; set; }
        public virtual List<Animal> Animals { get; set; }
        public virtual List<SubCategory> SubCategories { get; set; }
        public CreateCategoryCommand()
        {
            SubCategories = new List<SubCategory>();
            Animals = new List<Animal>();
        }
    }
}
