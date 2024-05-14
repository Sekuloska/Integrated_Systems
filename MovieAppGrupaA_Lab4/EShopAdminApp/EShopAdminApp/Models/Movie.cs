using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace EShopAdminApp.Models
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string MovieName { get; set; }
        
        public string MovieDescription { get; set; }
        
        public string MovieImage { get; set; }
       
        public double Rating { get; set; }

        public virtual ICollection<Ticket>? Tickets { get; set; }
    }
}
