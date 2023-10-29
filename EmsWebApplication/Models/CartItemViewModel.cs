using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmsWebApplication.Models
{
    public class CartItemViewModel
    {
        public int TicketId { get; set; }
        public string EventName { get; set; }
        public decimal EventPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get { return EventPrice * Quantity; } }
    }

    public class CartItem
    {
        public int TicketId { get; set; }
        public string EventName { get; set; }
        public decimal EventPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get { return EventPrice * Quantity; } }
    }
}