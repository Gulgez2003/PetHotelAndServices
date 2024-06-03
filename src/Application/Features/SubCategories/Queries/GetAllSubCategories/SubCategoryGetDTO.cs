namespace FinalProjectApp.Application.Features.SubCategories.Queries.GetAllSubCategories
{
    public class SubCategoryGetDTO
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
