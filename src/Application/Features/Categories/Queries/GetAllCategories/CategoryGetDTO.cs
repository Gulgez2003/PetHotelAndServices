namespace FinalProjectApp.Application.Features.Categories.Queries.GetAllCategories
{
    public class CategoryGetDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public virtual List<Animal> Animals { get; set; }
        public virtual List<SubCategory> SubCategories { get; set; }
    }
}
