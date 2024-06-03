namespace FinalProjectApp.Application.Features.Animals.Queries.GetAllAnimals
{
    public class AnimalGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public AnimalImage AnimalImage { get; set; }
        public virtual List<Category> Categories { get; set; }
    }
}
