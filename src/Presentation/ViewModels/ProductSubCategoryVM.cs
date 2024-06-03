namespace FinalProjectApp.Presentation.ViewModels
{
    public class ProductSubCategoryVM
    {
        public CreateProductCommand CreateCommand { get; set; }
        public UpdateProductCommand UpdateCommand { get; set; }
        public List<SubCategoryGetDTO> SubCategories { get; set; }  
        public IEnumerable<Colour>? Colours { get; set; }
    }
}
