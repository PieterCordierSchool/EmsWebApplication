using System;
using System.Collections.Generic;
using System.Web.Mvc;
using EmsWebApplication.Models;
using System.Linq;
using System.Data.Entity;


namespace EmsWebApplication.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddToCart(int ticketId)
        {
            var cart = GetCartFromSession();
            var ticket = db.Tickets.Find(ticketId);
            if (ticket != null)
            {
                cart.Add(ticket);
            }
            SaveCartToSession(cart);
            return RedirectToAction("Index");
        }

        private ShoppingCart GetCartFromSession()
        {
            ShoppingCart cart = Session["Cart"] as ShoppingCart;
            if (cart == null)
            {
                cart = new ShoppingCart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        private void SaveCartToSession(ShoppingCart cart)
        {
            Session["Cart"] = cart;
        }

        public class ShoppingCart
        {
            public List<Ticket> Items { get; set; }

            public ShoppingCart()
            {
                Items = new List<Ticket>();
            }

            public void Add(Ticket item)
            {
                Items.Add(item);
            }
        }
        [HttpPost]
    public ActionResult AddToCart(int eventId, int quantity)
    {
        var cart = GetCartFromSession();
        var eventToAdd = db.Events.FirstOrDefault(e => e.Id == eventId);
        if (eventToAdd != null)
        {
            cart.Items.Add(new ShoppingCartItem { Event = eventToAdd, Quantity = quantity, TotalPrice = eventToAdd.EventPrice * quantity });
            SaveCartToSession(cart);
        }

        return RedirectToAction("Index", "ShoppingCart");
    }
    }
}
