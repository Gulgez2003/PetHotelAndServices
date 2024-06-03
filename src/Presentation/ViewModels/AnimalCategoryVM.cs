namespace FinalProjectApp.Presentation.ViewModels
{
    public class AnimalCategoryVM
    {
        public CreateAnimalCommand CreateCommand { get; set; }
        public UpdateAnimalCommand UpdateCommand { get; set; }
        public List<CategoryGetDTO> Categories { get; set; }
    }
}
