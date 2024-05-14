namespace EShopAdminApp.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string? OwnerId { get; set; }
        public EApplicationUser? Owner { get; set; }
        public ICollection<TicketInOrder>? ProductInOrders { get; set; }
    }
}
