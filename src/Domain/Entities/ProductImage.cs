namespace Domain.Entities
{
    public class ProductImage : BaseEntity
    {
        public string Path { get; set; }
        public bool IsMain { get; set; }
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
