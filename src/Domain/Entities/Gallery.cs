namespace Domain.Entities
{
    public class Gallery : BaseAuditableEntity
    {
        public List<string> ImagePaths { get; set; }
        public Gallery()
        {
            ImagePaths = new List<string>();
        }
    }
}
