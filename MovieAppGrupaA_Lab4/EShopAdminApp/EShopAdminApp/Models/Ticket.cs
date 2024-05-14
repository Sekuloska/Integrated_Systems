﻿using System.ComponentModel.DataAnnotations;

namespace EShopAdminApp.Models
{
    public class Ticket
    {
        public Guid Id { get; set; }
        public Guid MovieId { get; set; }
        public Movie? Movie { get; set; }

        public double Price { get; set; }
        public double Rating { get; set; }
        public virtual EApplicationUser? CreatedBy { get; set; }
        public ICollection<TicketInOrder>? ProductInOrders { get; set; }
    }
}
