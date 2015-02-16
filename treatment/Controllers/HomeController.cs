using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using Rotativa;
using treatment.Models;
using treatment.Models.ViewModel;

namespace treatment.Controllers
{
    public class HomeController : Controller
    {
        private CommunityDBContext db = new CommunityDBContext();

        public string CurrentUserName
        {
            get
            {
                string name = "";

                if (System.Web.HttpContext.Current.Request.IsAuthenticated)
                {
                    name = Convert.ToString(System.Web.HttpContext.Current.User.Identity.Name.Split('|')[0]);
                }

                return name;
            }
        }

        public int CurrentClinicUser
        {
            get
            {
                int userClinicId = 0;

                if (System.Web.HttpContext.Current.Request.IsAuthenticated)
                {
                    userClinicId = Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name.Split('|')[2]);
                }

                return userClinicId;
            }
        }

        public ActionResult Index()
        {
            int user = CurrentClinicUser;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Login Page";

            return View();
        }
        [HttpPost]
        public ActionResult Login(Account anAccount)
        {
            //ViewBag.Message = "Login Page";

            int count =
                db.Accounts.Where(x => x.UserName == anAccount.UserName && x.Password == anAccount.Password).Count();
            if (count == 0)
            {
                ViewBag.Msg = "Invalid User";
                return View();
            }
            else if (count==1)
            {
               Account userAccount=new Account();
                var account =
                    db.Accounts.Where(x => x.UserName == anAccount.UserName && x.Password == anAccount.Password).ToList()
                        ;
                foreach (Account acc in account)
                {
                    userAccount.UserName = acc.UserName;
                    userAccount.UserRole = acc.UserRole;
                    userAccount.CommunityClinicID = acc.CommunityClinicID;
                    

                }

                FormsAuthentication.SetAuthCookie(userAccount.UserName + "|" + userAccount.UserRole + "|" + userAccount.CommunityClinicID, false);
                if (userAccount.UserRole=="Clinic") { return RedirectToAction("HomePageClinic", "Home"); }
                else if (userAccount.UserRole == "Head") { return RedirectToAction("HomePageHeadOffice", "Home"); }
                return RedirectToAction("Index", "Home");
            }
            //string name = "";

            //if (HttpContext.Current.Request.IsAuthenticated)
            //{
            //    name = Convert.ToInt32(HttpContext.Current.User.Identity.Name.Split('|')[0]);
            //}

            
            return View();
        }

        public ActionResult Logout()
        {
            //ViewBag.Message = "Login Page";
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
         [Authorize(Roles = "Head")]
        public ActionResult ChartIndex()
        {
            ViewBag.Districts = new List<District>(db.Districts);
            return View();
        }

        public ContentResult GetChartData(string fromDate,string toDate, string DistrictID)
        {
            DatabaseConnection aConnection=new DatabaseConnection();
            List<DiseaseReportViewModel> aViewModels = aConnection.GetChartData(fromDate, toDate, Convert.ToInt32(DistrictID));
            return Content(JsonConvert.SerializeObject(aViewModels));
        }

        public ContentResult GetDemographicData(string fromDate, string toDate, string DistrictName,string DiseaseName)
        {
            int DistrictID = db.Districts.Where(x => x.DistrictName == DistrictName).ToList().First().DistrictID;
            DatabaseConnection aConnection = new DatabaseConnection();
            List<DiseaseReportViewModel> aViewModels = aConnection.GetDemographicData(fromDate, toDate, Convert.ToInt32(DistrictID), DiseaseName);
            return Content(JsonConvert.SerializeObject(aViewModels));
        }

        public ActionResult DiseaseReport()
        {

            return View();
        }

        public ActionResult ShowDemographicMapSvg()
        {
            ViewBag.Desease = new List<Desease>(db.Deseases);
            
            return View();
        }
        
        public ActionResult GetDiseaseWiseReport()
        {
            ViewBag.Desease = new List<Desease>(db.Deseases);
            //return "Hello";
            return View();


        }


        public ActionResult GetDiseaseReportData(string deseaseID, string myPicker1, string myPicker2)
        {
            DatabaseConnection aDataConnection = new DatabaseConnection();
            List<DeseaseWiseReport> aDeseaseWiseReport = new List<DeseaseWiseReport>();
            Desease aDesease = new Desease();

            //DateTime myPicker;

            aDeseaseWiseReport = aDataConnection.GetDiseaseWiseData(deseaseID, myPicker1, myPicker2);


            return Json(aDeseaseWiseReport, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PDFAction(string fromDate,string toDate,string DiseaseID)
        {

            return new ActionAsPdf(
                 "ShowPDFReport",
                 new { fromDate = fromDate,toDate=toDate,DiseaseID=DiseaseID });

        }

        public ActionResult ShowPDFReport(string fromDate, string toDate, string DiseaseID)
        {
            DatabaseConnection aConnection=new DatabaseConnection();
            List<DeseaseWiseReport> aDeseaseWiseReports= aConnection.GetDiseaseWiseData(DiseaseID, fromDate, toDate);
            ViewBag.Report = aDeseaseWiseReports;
            return View();
        }
        [Authorize(Roles = "Clinic")]
        public ActionResult HomePageClinic()
        {
            return View();
        }

        [Authorize(Roles = "Head")]
        public ActionResult HomePageHeadOffice()
        {
            return View();
        }

        public ActionResult ShowDemographicMapSvgTest()
        {
            ViewBag.Desease = new List<Desease>(db.Deseases);

            return View();
        }

        public ActionResult GetAllDemographicData(string fromDate,string toDate,string DiseaseName)
        {
            DatabaseConnection aDatabaseConnection=new DatabaseConnection();
           List<DiseaseReportViewModel> aViewModels= aDatabaseConnection.GetAllDemographicData(fromDate, toDate, DiseaseName);
           return Json(aViewModels, JsonRequestBehavior.AllowGet);

        }


    }
}