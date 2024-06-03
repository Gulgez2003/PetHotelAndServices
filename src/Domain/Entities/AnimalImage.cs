namespace Domain.Entities
{
    public class AnimalImage : BaseEntity
    {
        public string Path { get; set; }
        public int? AnimalId { get; set; }
        public Animal? Animal { get; set; }
    }
}
