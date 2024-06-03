namespace FinalProjectApp.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int[] SelectedSubCategories { get; set; }
        public int[] SelectedAnimals { get; set; }
        public virtual List<Animal> Animals { get; set; }
        public virtual List<SubCategory> SubCategories { get; set; }
        public UpdateCategoryCommand()
        {
            SubCategories = new List<SubCategory>();
            Animals = new List<Animal>();
        }
    }
}
