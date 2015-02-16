using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using treatment.Models;
using treatment.Models.ViewModel;
using WebGrease.Css.Ast.Selectors;

namespace treatment.Controllers
{
    public class CommunityClinicController : Controller
    {

        private CommunityDBContext db = new CommunityDBContext();


        // GET: CommunityClinic
        public ActionResult Index()
        {
            var query = db.Thanas.Join(db.Districts,
                acc => acc.DistrictID,
                bank => bank.DistrictID,
                (acc, bank) => new {Account = acc, BankTransaction = bank}).ToList();

            var query1 =
                db.Thanas.Join(db.Districts, r => r.DistrictID, p => p.DistrictID,
                    (r, p) => new {r.DistrictID, p.DistrictName, r.ThanaID, r.ThanaName});

            var allData1 = from BT in db.Districts
                from B in db.Thanas
                from C in db.CommunityClinics
                where BT.DistrictID == B.DistrictID && C.ThanaID == B.ThanaID
                select new {BT.DistrictID, BT.DistrictName, B.ThanaID, B.ThanaName, C.CommunityClinicID, C.ClinicName};
            allData1.ToList();
            List<DistrictThanaDetails> districtThanaDetailseslisList = new List<DistrictThanaDetails>();
            foreach (var v1 in allData1)
            {
                DistrictThanaDetails aDistrictThanaDetails = new DistrictThanaDetails
                {
                    DistrictID = v1.DistrictID,
                    DistrictName = v1.DistrictName,
                    ThanaID = v1.ThanaID,
                    ThanaName = v1.ThanaName,
                    CommunityClinicID = v1.CommunityClinicID,
                    ClinicName = v1.ClinicName
                };
                districtThanaDetailseslisList.Add(aDistrictThanaDetails);
            }

            //       var query = db.Thanas.Join(db.Districts,
            //acc => acc.DistrictID,
            //bank => bank.DistrictID);

            var communityClinics = db.CommunityClinics.Include(c => c.Thana);
            //return View(communityClinics.ToList());
            return View(districtThanaDetailseslisList);
        }

        // GET: CommunityClinic/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThanaClinic aThanaClinic=new ThanaClinic();
            CommunityClinic communityClinic = db.CommunityClinics.Find(id);
            aThanaClinic.CommunityClinicID = communityClinic.CommunityClinicID;
            aThanaClinic.ClinicName = communityClinic.ClinicName;
            
            string thanaName = db.Thanas.Find(communityClinic.ThanaID).ThanaName;
            aThanaClinic.ThanaName = thanaName;
            if (communityClinic == null)
            {
                return HttpNotFound();
            }
            return View(aThanaClinic);
        }

        // GET: CommunityClinic/Create

        [Authorize(Roles = "Head")]
        public ActionResult Create()
        {

            ViewBag.DistrictID = new SelectList(db.Districts, "DistrictID", "DistrictName");
            ViewBag.Hello = new List<District>(db.Districts);


            return View();
        }

        // POST: CommunityClinic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "CommunityClinicID,ThanaID,ClinicName")] CommunityClinic communityClinic)
        public ActionResult Create(string ThanaID, string ClinicName)
        {

            CommunityClinic aCommunityClinic = new CommunityClinic();
            aCommunityClinic.ClinicName = ClinicName;
            aCommunityClinic.ThanaID = Convert.ToInt32(ThanaID);
            db.CommunityClinics.Add(aCommunityClinic);
            db.SaveChanges();
            int ClinicId = aCommunityClinic.CommunityClinicID;
            string user;
            string pass;
            //return RedirectToAction("SaveClinicResult");
            RandomNumberGenerator.Create("string");
           // pass = ClinicName + "_" + ThanaID;
            pass = GenerateRandomPassword();
            user = ClinicName.Replace(" ", string.Empty).ToLower() + "_" +
                   (db.CommunityClinics.OrderByDescending(x => x.CommunityClinicID).First().CommunityClinicID+1);
            int Thana = Convert.ToInt32(ThanaID);
            string user1 = db.Thanas.Where(x => x.ThanaID == Thana).First().ThanaName.ToLower() +
                           "_"+(db.CommunityClinics.OrderByDescending(x => x.CommunityClinicID).First().CommunityClinicID) +
                           1;
            TempData["user"] = user1;
            TempData["pass"] = pass;
            Account account = new Account() { UserName = user, Password = pass, UserRole = "Clinic", CommunityClinicID = ClinicId };
            db.Accounts.Add(account);
            db.SaveChanges();
            return Redirect("SaveClinicResult");

        }

        // GET: CommunityClinic/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommunityClinic communityClinic = db.CommunityClinics.Find(id);
            if (communityClinic == null)
            {
                return HttpNotFound();
            }
            ViewBag.ThanaID = new SelectList(db.Thanas, "ThanaID", "ThanaName", communityClinic.ThanaID);
            return View(communityClinic);
        }

        // POST: CommunityClinic/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(
            [Bind(Include = "CommunityClinicID,ThanaID,ClinicName")] CommunityClinic communityClinic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(communityClinic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ThanaID = new SelectList(db.Thanas, "ThanaID", "ThanaName", communityClinic.ThanaID);
            return View(communityClinic);
        }

        // GET: CommunityClinic/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            ThanaClinic aThanaClinic = new ThanaClinic();
            CommunityClinic communityClinic = db.CommunityClinics.Find(id);
            aThanaClinic.CommunityClinicID = communityClinic.CommunityClinicID;
            aThanaClinic.ClinicName = communityClinic.ClinicName;

            string thanaName = db.Thanas.Find(communityClinic.ThanaID).ThanaName;
            aThanaClinic.ThanaName = thanaName;


            //CommunityClinic communityClinic = db.CommunityClinics.Find(id);
            if (communityClinic == null)
            {
                return HttpNotFound();
            }
            return View(aThanaClinic);
        }

        // POST: CommunityClinic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommunityClinic communityClinic = db.CommunityClinics.Find(id);
            db.CommunityClinics.Remove(communityClinic);
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

        //[HttpPost]
        public ActionResult GetThanaList(string DistrictID)
        {
            int districtID = 0;
            if (DistrictID != "")
            {
                districtID = Convert.ToInt32(DistrictID);
            }
            var thanas = db.Thanas.Where(x => x.DistrictID == districtID).ToList();

            return Json(thanas, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetClinicList(string ThanaID)
        {
            int thanaID = 0;
            if (ThanaID != "")
            {
                thanaID = Convert.ToInt32(ThanaID);
            }
            var clinics = db.CommunityClinics.Where(x => x.ThanaID == thanaID).ToList();

            return Json(clinics, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DoctorSaveToClinic(DoctorClinic aDoctorClinic)
        {
            db.DoctorClinics.Add(aDoctorClinic);
            db.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }



        public ActionResult SaveClinic(string ThanaID, string ClinicName)
        {
            CommunityClinic aCommunityClinic = new CommunityClinic();
            aCommunityClinic.ClinicName = ClinicName;
            aCommunityClinic.ThanaID = Convert.ToInt32(ThanaID);
            db.CommunityClinics.Add(aCommunityClinic);
            db.SaveChanges();

            Console.WriteLine(Request.ServerVariables["http_referer"]);
            // return View("SaveClinicResult");
            //return Redirect("Index");
            return Index();
            //return RedirectToAction("Index");
        }

        public ActionResult SaveClinicResult()
        {
            ViewBag.User1 = TempData["user"];
            ViewBag.Pass1 = TempData["pass"];
            return View();

        }
        [Authorize(Roles = "Clinic")]
        public ActionResult MedicineStock()
        {
            int clinicId = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name.Split('|')[2]);
            
            string clinicName =
                db.CommunityClinics.Where(
                    x =>
                        x.CommunityClinicID ==clinicId
                        )
                    .ToList()
                    .First()
                    .ClinicName;
            ViewBag.Clinic = clinicName;
            DatabaseConnection aConnection=new DatabaseConnection();
            List<MedicineStockViewModel> aStock=aConnection.GetMedicineStock();

            return View(aStock);
        }



        private string GenerateRandomPassword()
        {
            string s = "";
            Random random = new Random();
            int length = 8;
            for (int i = 0; i < length; i++)
            {
                if (random.Next(0, 3) == 0) //if random.Next() == 0 then we generate a random character
                {
                    s += ((char)random.Next(65, 91)).ToString();
                }
                else //if random.Next() == 0 then we generate a random digit
                {
                    s += random.Next(0, 9);
                }
            }
            return s;
        }

    }


}



  


