using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using treatment.Models;

namespace treatment.Controllers
{
    public class ThanaController : Controller
    {
        private CommunityDBContext db = new CommunityDBContext();

        // GET: Thana
        public ActionResult Index()
        {
            var thanas = db.Thanas.Include(t => t.District);
            return View(thanas.ToList());
        }

        // GET: Thana/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thana thana = db.Thanas.Find(id);
            if (thana == null)
            {
                return HttpNotFound();
            }
            return View(thana);
        }
        [Authorize]
        // GET: Thana/Create
        public ActionResult Create()
        {
            ViewBag.DistrictID = new SelectList(db.Districts, "DistrictID", "DistrictName");
            return View();
        }

        // POST: Thana/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ThanaID,DistrictID,ThanaName")] Thana thana)
        {
            if (ModelState.IsValid)
            {
                db.Thanas.Add(thana);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DistrictID = new SelectList(db.Districts, "DistrictID", "DistrictName", thana.DistrictID);
            return View(thana);
        }

        // GET: Thana/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thana thana = db.Thanas.Find(id);
            if (thana == null)
            {
                return HttpNotFound();
            }
            ViewBag.DistrictID = new SelectList(db.Districts, "DistrictID", "DistrictName", thana.DistrictID);
            return View(thana);
        }

        // POST: Thana/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ThanaID,DistrictID,ThanaName")] Thana thana)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thana).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DistrictID = new SelectList(db.Districts, "DistrictID", "DistrictName", thana.DistrictID);
            return View(thana);
        }

        // GET: Thana/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Thana thana = db.Thanas.Find(id);
            if (thana == null)
            {
                return HttpNotFound();
            }
            return View(thana);
        }

        // POST: Thana/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Thana thana = db.Thanas.Find(id);
            db.Thanas.Remove(thana);
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
