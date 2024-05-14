namespace EShopAdminApplication.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public Guid ConcertId { get; set; }
        public Concert? Concert { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }
    }
}
