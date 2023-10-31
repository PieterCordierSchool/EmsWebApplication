using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmsWebApplication.Models;




namespace EmsWebApplication.Controllers
{
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private EmsWebApplicationDbEntities5 dbContext = new EmsWebApplicationDbEntities5(); // Renamed the variable to 'dbContext'


        // GET: Tickets
        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.Event);
            return View(tickets.ToList());
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            ViewBag.EventId = new SelectList(db.Events, "Id", "EventName");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Tickets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketId,EventId,UserId,PurchaseDate,Quantity,OriginalPrice,Discount,TotalPrice")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                // Add the new ticket to the database
                db.Tickets.Add(ticket);
                db.SaveChanges();

                // Optionally, you can add a success message to TempData
                TempData["SuccessMessage"] = "Ticket added successfully.";

                return RedirectToAction("Index");
            }

            // If the ModelState is not valid, return to the create view with the model
            ViewBag.EventId = new SelectList(db.Events, "Id", "EventName", ticket.EventId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventId = new SelectList(db.Events, "Id", "EventName", ticket.EventId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TicketId,EventId,UserId,PurchaseDate,Quantity,OriginalPrice,Discount,TotalPrice")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventId = new SelectList(db.Events, "Id", "EventName", ticket.EventId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult GetEventInfo(int eventId)
        {
            var selectedEvent = db.Events.Find(eventId);
            return PartialView("_EventInfoPartial", selectedEvent);
        }

        public ActionResult Checkout()
        {
            using (var context = new EmsWebApplicationDbEntities5())
            {
                // Logic for populating the database with dummy data
                Group newGroup = new Group
                {
                    GroupSize = 5, // Add your desired values here
                    DiscountPercentage = 10,
                    Coupon = "SOME_COUPON",
                    PromotionDiscount = 50,
                    Total = 100,
                    TotalAfterDiscount = 50
                };

                // Add the new group to the database
                context.Groups.Add(newGroup);
                context.SaveChanges();
            }

            return View("Confirmation"); // Show the confirmation view
        }




        [HttpPost]
        public ActionResult ProcessCheckout(CheckoutViewModel checkoutViewModel)
        {
            // Process the checkout and handle the provided shipping and billing information.
            // You can access the cart items in checkoutViewModel.CartItems.

            return View("CheckoutConfirmation"); // Redirect to a confirmation view.
        }

        public ActionResult Confirmation()
        {
            return View();
        }

        private List<CartItemViewModel> GetCartItemsFromSession()
        {
            var cartItems = HttpContext.Session["CartItems"] as List<CartItem>;
            if (cartItems != null)
            {
                // Convert CartItem objects to CartItemViewModel objects
                var cartItemViewModels = cartItems.Select(cartItem => new CartItemViewModel
                {
                    TicketId = cartItem.TicketId,
                    EventName = cartItem.EventName,
                    EventPrice = cartItem.EventPrice,
                    Quantity = cartItem.Quantity
                }).ToList();

                return cartItemViewModels;
            }

            return new List<CartItemViewModel>();
        }
        public ActionResult PopulateGroups()
        {
            try
            {
                var groups = new List<Group>
            {
                new Group { GroupSize = 5, DiscountPercentage = 10, Coupon = "ABC123", PromotionDiscount = 20, Total = 100, TotalAfterDiscount = 80 },
                new Group { GroupSize = 3, DiscountPercentage = 5, Coupon = "DEF456", PromotionDiscount = 10, Total = 50, TotalAfterDiscount = 45 },
                // Add more dummy records as needed
            };

                foreach (var group in groups)
                {
                    dbContext.Groups.Add(group);
                }

                dbContext.SaveChanges();

                return RedirectToAction("Index", "Groups"); // Redirect to the Groups index page
            }
            catch (Exception)
            {
                // Handle any exceptions that occur during the population process
                // You can log the exception or show an error message
                return View("Error");
            }
        }

    }
}


