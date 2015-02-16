using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml;


using Newtonsoft.Json;
using Rotativa;
using treatment.Models;
using treatment.Models.ViewModel;

using System.IO;

namespace treatment.Controllers
{
    public class TreatmentController : Controller
    {
        private CommunityDBContext db = new CommunityDBContext();

        // GET: /Treatment/
        public ActionResult Index()
        {
            var treatments = db.Treatments.Include(t => t.Doctor);
            return View(treatments.ToList());
        }

        // GET: /Treatment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treatment treatment = db.Treatments.Find(id);
            if (treatment == null)
            {
                return HttpNotFound();
            }
            return View(treatment);
        }
        [Authorize(Roles = "Clinic")]
        // GET: /Treatment/Create
        public ActionResult Create()
        {
            DatabaseConnection aConnection=new DatabaseConnection();
            int clinicID = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name.Split('|')[2]);
            //ViewBag.DoctorID = new List<Doctor>(db.Doctors);
            ViewBag.DoctorID = aConnection.GetDoctors();
            ViewBag.Clinic = clinicID;
                //Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name.Split('|')[2]);
            //var clinic =
            //    db.CommunityClinics.Where(x =>
            //            x.CommunityClinicID ==
            //                Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name.Split('|')[2])).Select(y=>y.ClinicName).ToString();
            ViewBag.MedicineDoseID = new List<MedicineDose>(db.MedicineDoses);
            string uri = "http://nerdcastlebd.com/web_service/voterdb/index.php/voters/voter/5644309456813";

            HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(response.GetResponseStream());
            XmlNodeList nodes = xmlDoc.DocumentElement.SelectNodes("voter");
            Voter aVoter=new Voter();
            foreach (XmlNode node in nodes)
            {
                aVoter.VoterNumber = node.SelectSingleNode("id").InnerText;
                aVoter.Name = node.SelectSingleNode("name").InnerText;
                aVoter.Address = node.SelectSingleNode("address").InnerText;
                aVoter.DateOFBirth = node.SelectSingleNode("date_of_birth").InnerText;
            }
            string jsonText = JsonConvert.SerializeXmlNode(xmlDoc);
            string json = Newtonsoft.Json.JsonConvert.SerializeXmlNode(xmlDoc);

            ViewBag.MedicineID = new List<Medicine>(db.Medicines);
           
            List<MedicineStockViewModel> aClinicStock = aConnection.GetMedicineStock();
            ViewBag.MedicineID1 = aClinicStock;
            ViewBag.Desease = new List<Desease>(db.Deseases);
            //ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "DoctorName");
            //ViewBag.VoterID = new SelectList(db.Voters, "VoterID", "Name");
            return View();
        }

        // POST: /Treatment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include="TreatmentID,CenterID,DoctorID,VoterID,Observation,Date")] Treatment treatment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Treatments.Add(treatment);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "DoctorName", treatment.DoctorID);
        //    //ViewBag.VoterID = new SelectList(db.Voters, "VoterID", "Name", treatment.VoterID);
        //    return View(treatment);
        //}


        [HttpPost]
        
        public ActionResult Create(TreatmentViewModel aModel)
        {

            //foreach (MedicineClinic medicineClinic in json)
            //{
            //    db.MedicineClinics.Add(medicineClinic);
            //    db.SaveChanges();
            //}
            //Treatment aTreatment = aModel.ATreatment;

            //db.Treatments.Add(aTreatment);
            //db.SaveChanges();
            //int treatmentID = aTreatment.TreatmentID;
            //List<TreatmentMedicine> aTreatmentMedicineList = aModel.ATreatmentMedicinesList;
            //TempData["TreatmentMeds"] = aTreatmentMedicineList;
            //foreach (TreatmentMedicine treatmentMedicine in aTreatmentMedicineList)
            //{
            //    treatmentMedicine.TreatmentID = treatmentID;
            //    db.TreatmentMedicines.Add(treatmentMedicine);
            //    db.SaveChanges();
            //}



            //Treatment aTreatment = aModel.ATreatment;

            //db.Treatments.Add(aTreatment);
            //db.SaveChanges();
            //int treatmentID = aTreatment.TreatmentID;
            //List<TreatmentMedicine> aTreatmentMedicineList = aModel.ATreatmentMedicinesList;
            //TempData["TreatmentMeds"] = aTreatmentMedicineList;
            //foreach (TreatmentMedicine treatmentMedicine in aTreatmentMedicineList)
            //{
            //    treatmentMedicine.TreatmentID = treatmentID;
            //    db.TreatmentMedicines.Add(treatmentMedicine);
            //    db.SaveChanges();
            //}

            return Redirect("SomeAction");
            ////return RedirectToAction("OrdersInPdf");
            //return new Rotativa.ActionAsPdf("ShowReport");



        }




        public ActionResult SaveTreatment(TreatmentViewModel aModel)
        {

           DatabaseConnection aConnection=new DatabaseConnection();
            Treatment aTreatment = aModel.ATreatment;

            db.Treatments.Add(aTreatment);
            db.SaveChanges();
            int treatmentID = aTreatment.TreatmentID;
            int clinicID = aTreatment.CenterID;
            List<TreatmentMedicine> aTreatmentMedicineList = aModel.ATreatmentMedicinesList;
            TempData["TreatmentMeds"] = aTreatmentMedicineList;
            foreach (TreatmentMedicine treatmentMedicine in aTreatmentMedicineList)
            {
                treatmentMedicine.TreatmentID = treatmentID;
                db.TreatmentMedicines.Add(treatmentMedicine);
                db.SaveChanges();
               // db.MedicineClinics.AddOrUpdate(s => s.Quantity = s.Quantity-treatmentMedicine.QtyGiven, s => s. == 4);
                aConnection.UpdateMedicineStock(clinicID,treatmentMedicine.MedicineId,treatmentMedicine.QtyGiven);
            }

            return Json(treatmentID, JsonRequestBehavior.AllowGet);
          

        }

        public ActionResult SomeAction(string treatmentID)
        {
            
            return new ActionAsPdf(
                 "ShowReport",
                 new { treatmentID = treatmentID });
           
        }


        // GET: /Treatment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treatment treatment = db.Treatments.Find(id);
            if (treatment == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "DoctorName", treatment.DoctorID);
            //ViewBag.VoterID = new SelectList(db.Voters, "VoterID", "Name", treatment.VoterID);
            return View(treatment);
        }





        // POST: /Treatment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TreatmentID,CenterID,DoctorID,VoterID,Observation,Date")] Treatment treatment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(treatment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "DoctorName", treatment.DoctorID);
            //ViewBag.VoterID = new SelectList(db.Voters, "VoterID", "Name", treatment.VoterID);
            return View(treatment);
        }

        // GET: /Treatment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Treatment treatment = db.Treatments.Find(id);
            if (treatment == null)
            {
                return HttpNotFound();
            }
            return View(treatment);
        }

        // POST: /Treatment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Treatment treatment = db.Treatments.Find(id);
            db.Treatments.Remove(treatment);
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

        

        public ActionResult GetVoterFromWebService(string voter_id)
        {
            string uri = "http://nerdcastlebd.com/web_service/voterdb/index.php/voters/voter/"+voter_id+"";

            HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(response.GetResponseStream());
            XmlNodeList nodes = xmlDoc.DocumentElement.SelectNodes("voter");
            VoterTreatmentViewModel aVoter = new VoterTreatmentViewModel();
            foreach (XmlNode node in nodes)
            {
                aVoter.VoterNumber = node.SelectSingleNode("id").InnerText;
                aVoter.Name = node.SelectSingleNode("name").InnerText;
                aVoter.Address = node.SelectSingleNode("address").InnerText;
                aVoter.DateOFBirth = node.SelectSingleNode("date_of_birth").InnerText;
            }

            int treatmentCount = db.Treatments.Where(x => x.VoterNumber == aVoter.VoterNumber).ToList().Count;
            aVoter.Treatmentcount = treatmentCount;
            List<VoterTreatmentViewModel> aList = new List<VoterTreatmentViewModel>();
            if (aVoter.VoterNumber != "")
            {
                aList.Add(aVoter);
            }

            return Json(aList, JsonRequestBehavior.AllowGet);
            
        }

        public ActionResult GetVoterHistory(string voter_id)
        {
            
            DatabaseConnection aConnection=new DatabaseConnection();
            List<TreatmentHistory> treatmentHistoryList = aConnection.GeTreatmentHistories(voter_id);



            return Json(treatmentHistoryList, JsonRequestBehavior.AllowGet);
        }
   

        

        public ActionResult ShowReport(string treatmentID)
        {
            //string treatmentID = TempData["tID"] as string;
            ViewBag.tID = treatmentID;
            int treatId = Convert.ToInt32(treatmentID);
            

            
            
            
            Treatment aTreatment = db.Treatments.Where(x => x.TreatmentID == treatId).ToList().First();
            List<TreatmentMedicine> aList = db.TreatmentMedicines.Where(x => x.TreatmentID == treatId).ToList();
            // return View();
            TempData["TreatmentMeds"] = aList;
            TempData["TreatmentSummary"] = aTreatment;
            ViewBag.DoctorName = db.Doctors.Where(x => x.DoctorID == aTreatment.DoctorID).ToList().First().DoctorName;
            ViewBag.CenterName =
                db.CommunityClinics.Where(x => x.CommunityClinicID == aTreatment.CenterID).First().ClinicName;
            string voter_id = db.Treatments.Where(x => x.TreatmentID == aTreatment.TreatmentID).First().VoterNumber;
            ViewBag.Observation = aTreatment.Observation;
            ViewBag.Disease = db.Deseases.Where(x => x.DeseaseID == aTreatment.DeseaseID).First().DeseaseName;
            string uri = "http://nerdcastlebd.com/web_service/voterdb/index.php/voters/voter/" + voter_id + "";
            HttpWebRequest request = WebRequest.Create(uri) as HttpWebRequest;
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(response.GetResponseStream());
            XmlNodeList nodes = xmlDoc.DocumentElement.SelectNodes("voter");
            VoterTreatmentViewModel aVoter = new VoterTreatmentViewModel();
            foreach (XmlNode node in nodes)
            {
                aVoter.VoterNumber = node.SelectSingleNode("id").InnerText;
                aVoter.Name = node.SelectSingleNode("name").InnerText;
                aVoter.Address = node.SelectSingleNode("address").InnerText;
                aVoter.DateOFBirth = node.SelectSingleNode("date_of_birth").InnerText;
            }
            ViewBag.PatientInfo = aVoter;


            List<TreatmentMedicine> aTreatmentMedicines = TempData["TreatmentMeds"] as List<TreatmentMedicine>;
            return View(aTreatmentMedicines);
        }


    }
}
