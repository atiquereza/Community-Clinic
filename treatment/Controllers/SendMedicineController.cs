using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using treatment.Models;

namespace treatment.Controllers
{
    public class SendMedicineController : Controller
    {
        private CommunityDBContext db = new CommunityDBContext();

        // GET: SendMedicine
        public ActionResult Index()
        {
            return View();
        }

        // GET: SendMedicine/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [Authorize]
        // GET: SendMedicine/Create
        public ActionResult Create()
        {
            ViewBag.DistrictID = new List<District>(db.Districts);
            ViewBag.MedicineID = new List<Medicine>(db.Medicines);
            return View();
        }

        // POST: SendMedicine/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}


        [HttpPost]
        public ActionResult Create(ICollection<MedicineClinic> json)
        {
            DatabaseConnection aConnection=new DatabaseConnection();

            foreach (MedicineClinic medicineClinic in json)
            {
                aConnection.UpdateMedicineHeadOffice(medicineClinic.Medicine_MedicineID,medicineClinic.Quantity);
                int chk =
                    db.MedicineClinics.Where(
                        x =>
                            x.CommunityClinicID == medicineClinic.CommunityClinicID &&
                            x.Medicine_MedicineID == medicineClinic.Medicine_MedicineID).ToList().Count;
                if (chk == 0)
                {
                    db.MedicineClinics.Add(medicineClinic);
                    db.SaveChanges();
                }
                else
                {
                    aConnection.UpdateMedicineDistributionClinic(medicineClinic.CommunityClinicID,medicineClinic.Medicine_MedicineID,medicineClinic.Quantity);
                    
                }
            }

  
            ViewBag.DistrictID = new List<District>(db.Districts);
            ViewBag.MedicineID = new List<Medicine>(db.Medicines);
            
            return View();



        }

        // GET: SendMedicine/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SendMedicine/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: SendMedicine/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SendMedicine/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
