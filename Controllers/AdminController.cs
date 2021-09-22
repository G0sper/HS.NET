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
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View(db.iCarts.ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            iCarts iCarts = db.iCarts.Find(id);
            if (iCarts == null)
            {
                return HttpNotFound();
            }
            return View(iCarts);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,account_id,product_id,product_name,product_descript,product_price,product_qua,product_img")] iCarts iCarts)
        {
            if (ModelState.IsValid)
            {
                db.iCarts.Add(iCarts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(iCarts);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            iCarts iCarts = db.iCarts.Find(id);
            if (iCarts == null)
            {
                return HttpNotFound();
            }
            return View(iCarts);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,account_id,product_id,product_name,product_descript,product_price,product_qua,product_img")] iCarts iCarts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iCarts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(iCarts);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            iCarts iCarts = db.iCarts.Find(id);
            if (iCarts == null)
            {
                return HttpNotFound();
            }
            return View(iCarts);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            iCarts iCarts = db.iCarts.Find(id);
            db.iCarts.Remove(iCarts);
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
