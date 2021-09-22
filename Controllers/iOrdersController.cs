using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HS.NET.Models;

namespace HS.NET.Controllers
{
    public class iOrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: iOrders
        public ActionResult Index()
        {
            return View(db.iOrders.ToList());
        }

        // GET: iOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            iOrders iOrders = db.iOrders.Find(id);
            if (iOrders == null)
            {
                return HttpNotFound();
            }
            return View(iOrders);
        }

        // GET: iOrders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: iOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,product_id,product_name,account_id,order_delivery,order_price")] iOrders iOrders)
        {
            if (ModelState.IsValid)
            {
                db.iOrders.Add(iOrders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(iOrders);
        }

        // GET: iOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            iOrders iOrders = db.iOrders.Find(id);
            if (iOrders == null)
            {
                return HttpNotFound();
            }
            return View(iOrders);
        }

        // POST: iOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,product_id,product_name,account_id,order_delivery,order_price")] iOrders iOrders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iOrders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(iOrders);
        }

        // GET: iOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            iOrders iOrders = db.iOrders.Find(id);
            if (iOrders == null)
            {
                return HttpNotFound();
            }
            return View(iOrders);
        }

        // POST: iOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            iOrders iOrders = db.iOrders.Find(id);
            db.iOrders.Remove(iOrders);
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
