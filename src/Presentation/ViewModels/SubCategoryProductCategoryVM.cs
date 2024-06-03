namespace FinalProjectApp.Presentation.ViewModels
{
    public class SubCategoryProductCategoryVM
    {
        public CreateSubCategoryCommand CreateCommand { get; set; }
        public UpdateSubCategoryCommand UpdateCommand { get; set; }
        public List<CategoryGetDTO> Categories { get; set; }
        public List<ProductGetDTO> Products { get; set; }
    }
}
