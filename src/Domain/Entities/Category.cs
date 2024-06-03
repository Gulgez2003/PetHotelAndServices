namespace Domain.Entities
{
    public class Category : BaseAuditableEntity
    {
        public string? Name { get; set; }
        public virtual List<Animal> Animals { get; set; }
        public virtual List<SubCategory> SubCategories { get; set; }

        public Category()
        {
            SubCategories = new List<SubCategory>();
            Animals = new List<Animal>();
        }
    }
}
