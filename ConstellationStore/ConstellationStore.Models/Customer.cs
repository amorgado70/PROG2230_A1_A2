namespace ConstellationStore.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string PictureUrl { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Town { get; set; }
        public string PostalCode { get; set; }
        public string HomePhone { get; set; }
        public string BusinessPhone { get; set; }
        public string EmailAddress { get; set; }
    }
}
