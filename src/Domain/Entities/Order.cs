namespace Domain.Entities
{
    public class Order : BaseAuditableEntity
    {
        public List<Product> Products { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Message { get; set; }
        public bool IsCash { get; set; }
        public bool IsCardOnline { get; set; }
        public bool IsCardOnReceipt { get; set; }
        
        public Order()
        {
            Products = new List<Product>();
        }
    }
}
