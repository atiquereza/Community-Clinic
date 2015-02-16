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
    public class DeseaseController : Controller
    {
        private CommunityDBContext db = new CommunityDBContext();

        // GET: Desease
        public ActionResult Index()
        {
            return View(db.Deseases.ToList());
        }

        // GET: Desease/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Desease desease = db.Deseases.Find(id);
            if (desease == null)
            {
                return HttpNotFound();
            }
            return View(desease);
        }
        [Authorize]
        // GET: Desease/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Desease/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DeseaseID,DeseaseName,TreatmentProcedure,PreferedMedicines")] Desease desease)
        {
            if (ModelState.IsValid)
            {
                db.Deseases.Add(desease);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(desease);
        }

        // GET: Desease/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Desease desease = db.Deseases.Find(id);
            if (desease == null)
            {
                return HttpNotFound();
            }
            return View(desease);
        }

        // POST: Desease/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DeseaseID,DeseaseName,TreatmentProcedure,PreferedMedicines")] Desease desease)
        {
            if (ModelState.IsValid)
            {
                db.Entry(desease).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(desease);
        }

        // GET: Desease/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Desease desease = db.Deseases.Find(id);
            if (desease == null)
            {
                return HttpNotFound();
            }
            return View(desease);
        }

        // POST: Desease/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Desease desease = db.Deseases.Find(id);
            db.Deseases.Remove(desease);
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
