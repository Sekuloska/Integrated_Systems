using EShop.Domain.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.DTO
{
    public class ConcertDTO
    {
        public Guid Id { get; set; }
        public string ConcertName { get; set; }
        public string ConcertDescription { get; set; }
        public string ConcertImage { get; set; }
        public double Rating { get; set; }
        public virtual ICollection<Ticket>? Tickets { get; set; }
    }
}
