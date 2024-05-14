namespace EShopAdminApplication.Models
{
    public class Concert
    {
        public Guid Id { get; set; }
        public string ConcertName { get; set; }
        public string ConcerDescription { get; set; }
        public string ConcerImage { get; set; }
        public double Rating { get; set; }
    }
}
