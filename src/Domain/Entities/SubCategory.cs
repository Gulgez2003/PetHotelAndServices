namespace Domain.Entities
{
    public class SubCategory : BaseAuditableEntity
    {
        public string Name { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public virtual List<Product>? Products { get; set; }

        public SubCategory()
        {
            Products = new List<Product>();
        }
    }
}
