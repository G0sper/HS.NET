using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HS.NET.Models;
using Microsoft.AspNet.Identity;

namespace HS.NET.Controllers
{
    public class iCartsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: iCarts
        public async Task<ActionResult> Index()
        {
            if (Request.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId().ToString();


                return View(await db.iCarts.Where(p => p.account_id == userId).ToListAsync());
            }
            else
            {
                return Redirect("/");
            }
        }

        [HttpPost]
        public FileResult ExportData()
        {
            ApplicationDbContext entities = new ApplicationDbContext();
            List<object> Orders = (from customer in entities.iOrders.ToList()
                                   select new[] { customer.id.ToString(),
                                                            customer.account_id.ToString(),
                                                            customer.product_id.ToString(),
                                                            customer.product_name.ToString(),
                                                            customer.order_price.ToString(),
                                                            customer.order_delivery.ToString(),
                                }).ToList<object>();

            //Insert the Column Names.
            Orders.Insert(0, new string[6] { "Id", "Customer ID", "Product ID", "Product Name", "Product Price", "Product Delivery" });

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Orders.Count; i++)
            {
                string[] customer = (string[])Orders[i];
                for (int j = 0; j < customer.Length; j++)
                {
                    //Append data with separator.
                    sb.Append(customer[j] + ',');
                }

                //Append new line character.
                sb.Append("\r\n");

            }

            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "SoftOrders.csv");
        }


        public ActionResult OrderCarts()
        {
            var userId = User.Identity.GetUserId().ToString();

            List<iCarts> iCartsAsync = db.iCarts.Where(p => p.account_id == userId).ToList();

            foreach (var item in iCartsAsync)
            {
                iOrders order = new iOrders();

                DateTime localDate = DateTime.Now;
                localDate.AddDays(3);

                order.product_id = item.product_id;
                order.product_name = item.product_name;
                order.account_id = item.account_id;
                order.order_price = item.product_price;
                order.order_delivery = localDate.ToString("MM/dd/yyyy");

                db.iOrders.Add(order);
                db.SaveChanges();

                db.iCarts.Remove(item);
                db.SaveChanges();
            }

            return Redirect("/iCarts");
        }


        // GET: iCarts/Details/5
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

        // GET: iCarts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: iCarts/Create
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

        // GET: iCarts/Edit/5
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

        // POST: iCarts/Edit/5
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

        // GET: iCarts/Delete/5
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

        // POST: iCarts/Delete/5
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
