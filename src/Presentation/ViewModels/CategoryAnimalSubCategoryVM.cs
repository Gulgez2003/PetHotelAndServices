namespace FinalProjectApp.Presentation.ViewModels
{
    public class CategoryAnimalSubCategoryVM
    {
        public CreateCategoryCommand CreateCommand { get; set; }
        public UpdateCategoryCommand UpdateCommand { get; set; }
        public List<AnimalGetDTO> Animals { get; set; }
        public List<SubCategoryGetDTO> SubCategories { get; set; }
    }
}
