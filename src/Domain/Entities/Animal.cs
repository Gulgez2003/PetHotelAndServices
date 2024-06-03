namespace Domain.Entities
{
    public class Animal : BaseAuditableEntity
    {
        public string Name { get; set; }
        public AnimalImage AnimalImage { get; set; }
        public virtual List<Category> Categories { get; set; }

        public Animal()
        {
            Categories = new List<Category>();
        }
    }
}
