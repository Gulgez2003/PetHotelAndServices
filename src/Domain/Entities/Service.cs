namespace FinalProjectApp.Domain.Entities
{
    public class Service : BaseAuditableEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
