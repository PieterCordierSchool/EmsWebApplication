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
    public class TicketSalesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TicketSales
        public ActionResult Index()
        {
            var ticketSales = db.TicketSales.Include(t => t.Ticket);
            return View(ticketSales.ToList());
        }

        // GET: TicketSales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketSale ticketSale = db.TicketSales.Find(id);
            if (ticketSale == null)
            {
                return HttpNotFound();
            }
            return View(ticketSale);
        }

        // GET: TicketSales/Create
        public ActionResult Create()
        {
            ViewBag.TicketId = new SelectList(db.Tickets, "TicketId", "UserId");
            return View();
        }

        // POST: TicketSales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TicketSaleId,TicketId,UserId,PurchaseDate,Quantity,TotalPrice")] TicketSale ticketSale)
        {
            if (ModelState.IsValid)
            {
                db.TicketSales.Add(ticketSale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TicketId = new SelectList(db.Tickets, "TicketId", "UserId", ticketSale.TicketId);
            return View(ticketSale);
        }

        // GET: TicketSales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketSale ticketSale = db.TicketSales.Find(id);
            if (ticketSale == null)
            {
                return HttpNotFound();
            }
            ViewBag.TicketId = new SelectList(db.Tickets, "TicketId", "UserId", ticketSale.TicketId);
            return View(ticketSale);
        }

        // POST: TicketSales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TicketSaleId,TicketId,UserId,PurchaseDate,Quantity,TotalPrice")] TicketSale ticketSale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketSale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TicketId = new SelectList(db.Tickets, "TicketId", "UserId", ticketSale.TicketId);
            return View(ticketSale);
        }

        // GET: TicketSales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketSale ticketSale = db.TicketSales.Find(id);
            if (ticketSale == null)
            {
                return HttpNotFound();
            }
            return View(ticketSale);
        }

        // POST: TicketSales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketSale ticketSale = db.TicketSales.Find(id);
            db.TicketSales.Remove(ticketSale);
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
    }
}
