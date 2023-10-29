﻿using System;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketId,EventId,UserId,PurchaseDate,Quantity,OriginalPrice,Discount,TotalPrice")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
            var checkoutViewModel = new CheckoutViewModel
            {
                CartItems = GetCartItemsFromSession(), // Correctly assign cart items
                                                       // Other properties for the checkoutViewModel
            };

            return View(checkoutViewModel);
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
    }
}


