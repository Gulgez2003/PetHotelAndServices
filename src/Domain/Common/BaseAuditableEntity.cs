namespace Domain.Common
{
    public class BaseAuditableEntity : BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set;}
        public string CreatedBy {  get; set; }
        public string? LastModifiedBy { get; set;}
    }
}
