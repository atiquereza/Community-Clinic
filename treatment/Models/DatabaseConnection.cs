using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using treatment.Models.ViewModel;

namespace treatment.Models
{
    public class DatabaseConnection
    {

        string connectionStr = ConfigurationManager.ConnectionStrings["CommunityMedicineDBContext"].ConnectionString;
        private SqlConnection aSqlConnection;
        private SqlCommand aSqlCommand;
        public DatabaseConnection()
        {
            aSqlConnection = new SqlConnection(connectionStr);

        }

        public List<TreatmentHistory> GeTreatmentHistories(string voter_id)
        {
            string query =
                "select ClinicName,DeseaseName,observation,DATE,voterNumber from CommunityClinics,Deseases,Treatments where Treatments.DeseaseID=Deseases.DeseaseID and Treatments.CenterID=CommunityClinics.CommunityClinicID and VoterNumber='" +
                voter_id + "'";
            aSqlConnection.Open();
            aSqlCommand = new SqlCommand(query, aSqlConnection);
            SqlDataReader aDataReader = aSqlCommand.ExecuteReader();

            List<TreatmentHistory> aList = new List<TreatmentHistory>();

            while (aDataReader.Read())
            {
                //Designation aDesignation = new Designation();
                TreatmentHistory aTreatmentHistory=new TreatmentHistory();
                aTreatmentHistory.ClinicName = aDataReader["ClinicName"].ToString();
                aTreatmentHistory.DeseaseName = aDataReader["DeseaseName"].ToString();
                aTreatmentHistory.Observation = aDataReader["Observation"].ToString();
                aTreatmentHistory.Date =Convert.ToString(aDataReader["Date"]);
                
                aTreatmentHistory.VoterNumber = aDataReader["VoterNumber"].ToString();

                aList.Add(aTreatmentHistory);
            }
            aDataReader.Close();
            aSqlConnection.Close();
            return aList;

        }

        public List<Doctor> GetDoctors()
        {
            string query = string.Format("select Doctors.DoctorID,DoctorName from Doctors,DoctorClinics where Doctors.DoctorID=DoctorClinics.DoctorID and DoctorClinics.CommunityClinicID={0}", Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name.Split('|')[2]));
            aSqlConnection.Open();
            aSqlCommand = new SqlCommand(query, aSqlConnection);
            SqlDataReader aDataReader = aSqlCommand.ExecuteReader();

            List<Doctor> aList = new List<Doctor>();

            while (aDataReader.Read())
            {
                //Designation aDesignation = new Designation();
                Doctor aDoctor = new Doctor();
                aDoctor.DoctorID = Convert.ToInt32(aDataReader[0]);
                aDoctor.DoctorName = aDataReader[1].ToString();
               
                aList.Add(aDoctor);
            }
            aDataReader.Close();
            aSqlConnection.Close();
            return aList;
        }

        public List<MedicineStockViewModel> GetMedicineStock()
        {

            string query = string.Format("select MedicineName,WeightContaints,MG_ML,MedicineClinics.Quantity,Medicines.MedicineID from MedicineClinics,Medicines where MedicineClinics.Medicine_MedicineID=Medicines.MedicineID and MedicineClinics.CommunityClinicID={0}", Convert.ToInt32(System.Web.HttpContext.Current.User.Identity.Name.Split('|')[2]));
            aSqlConnection.Open();
            aSqlCommand = new SqlCommand(query, aSqlConnection);
            SqlDataReader aDataReader = aSqlCommand.ExecuteReader();

            List<MedicineStockViewModel> aList = new List<MedicineStockViewModel>();

            while (aDataReader.Read())
            {
                //Designation aDesignation = new Designation();
                MedicineStockViewModel aStockViewModel = new MedicineStockViewModel();
                aStockViewModel.MedicineName= Convert.ToString(aDataReader[0]);
                aStockViewModel.WeightContains = Convert.ToString(aDataReader[1]);
                aStockViewModel.MG_ML = Convert.ToString(aDataReader[2]);
                aStockViewModel.Quantity = Convert.ToInt32(aDataReader[3]);
                aStockViewModel.MedicineId = Convert.ToInt32(aDataReader[4]);
                aList.Add(aStockViewModel);
            }
            aDataReader.Close();
            aSqlConnection.Close();
            return aList;

        }

        public List<DiseaseReportViewModel> GetChartData(string fromDate, string toDate, int DistrictID)
        {
            string q1 =
                "select fin.DistrictName,NoOfPatient,Districts.DistrictID,Districts.population,(NoOfPatient/Districts.Population)*100 as pct,DeseaseName from (select DistrictName,COUNT(voternumber) as NoOfPatient,DeseaseName";
            String q2 =
                "from (select DistrictName,ThanaName,ClinicName,voterNumber,DeseaseName from CommunityClinics,Districts,Deseases,Thanas,(SELECT * FROM Treatments";
            String q3 = string.Format("WHERE date >= '{0}'AND date <  '{1}') a where a.DeseaseID=Deseases.DeseaseID and a.CenterID=CommunityClinics.CommunityClinicID and CommunityClinics.ThanaID=Thanas.ThanaID and Thanas.DistrictID=Districts.DistrictID)abc group by DistrictName,DeseaseName ) fin,Districts where fin.DistrictName=Districts.DistrictName and Districts.DistrictID={2}",fromDate,toDate,DistrictID);

            string query = q1 + q2 + q3;

            string query1 = string.Format("select fin.DistrictName,NoOfPatient,Districts.DistrictID,Districts.population,(NoOfPatient/Districts.Population)*100 as pct,DeseaseName from (select DistrictName,COUNT(voternumber) as NoOfPatient,DeseaseName from (select DistrictName,ThanaName,ClinicName,voterNumber,DeseaseName from CommunityClinics,Districts,Deseases,Thanas,(SELECT * FROM Treatments WHERE date >= '{0}' AND date <  '{1}') a where a.DeseaseID=Deseases.DeseaseID and a.CenterID=CommunityClinics.CommunityClinicID and CommunityClinics.ThanaID=Thanas.ThanaID and Thanas.DistrictID=Districts.DistrictID)abc group by DistrictName,DeseaseName ) fin,Districts where fin.DistrictName=Districts.DistrictName and Districts.DistrictID={2}", fromDate, toDate, DistrictID);
            string query2 = string.Format("select distinctCheck.DistrictName,COUNT(*) as NoOfPatient,DistrictID,distinctCheck.population,distinctCheck.pct,distinctCheck.DeseaseName from (select fin.DistrictName,NoOfPatient,Districts.DistrictID,Districts.population,(NoOfPatient/Districts.Population)*100 as pct,DeseaseName from (select DistrictName,COUNT(voternumber) as NoOfPatient,DeseaseName from (select DistrictName,ThanaName,ClinicName,voterNumber,DeseaseName from CommunityClinics,Districts,Deseases,Thanas,(SELECT * FROM Treatments WHERE date >= '{0}' AND date <  '{1}') a where a.DeseaseID=Deseases.DeseaseID and a.CenterID=CommunityClinics.CommunityClinicID and CommunityClinics.ThanaID=Thanas.ThanaID and Thanas.DistrictID=Districts.DistrictID)abc group by DistrictName,DeseaseName,voterNumber ) fin,Districts where fin.DistrictName=Districts.DistrictName and Districts.DistrictID={2}) distinctCheck group by distinctCheck.DistrictName,DistrictID,distinctCheck.population,distinctCheck.pct,distinctCheck.DeseaseName", fromDate, toDate, DistrictID);
            aSqlConnection.Open();
            aSqlCommand = new SqlCommand(query2, aSqlConnection);
            SqlDataReader aDataReader = aSqlCommand.ExecuteReader();

            List<DiseaseReportViewModel> aList = new List<DiseaseReportViewModel>();

            while (aDataReader.Read())
            {
                //Designation aDesignation = new Designation();
                DiseaseReportViewModel aDiseaseModel = new DiseaseReportViewModel();
                aDiseaseModel.NoOfPatient = Convert.ToInt32(aDataReader["NoOfPatient"]);
                aDiseaseModel.DeseaseName = Convert.ToString(aDataReader["DeseaseName"]);
                aList.Add(aDiseaseModel);
            }
            aDataReader.Close();
            aSqlConnection.Close();
            return aList;
        }















        public List<DiseaseReportViewModel> GetDemographicData(string fromDate, string toDate, int DistrictId, string DiseaseName)
        {
            string q1 =
                "select fin.DistrictName,NoOfPatient,Districts.DistrictID,Districts.population,(NoOfPatient/Districts.Population)*100 as pct,DeseaseName from (select DistrictName,COUNT(voternumber) as NoOfPatient,DeseaseName";
            String q2 =
                "from (select DistrictName,ThanaName,ClinicName,voterNumber,DeseaseName from CommunityClinics,Districts,Deseases,Thanas,(SELECT * FROM Treatments";
            String q3 = string.Format("WHERE date >= '{0}'AND date <  '{1}') a where a.DeseaseID=Deseases.DeseaseID and a.CenterID=CommunityClinics.CommunityClinicID and CommunityClinics.ThanaID=Thanas.ThanaID and Thanas.DistrictID=Districts.DistrictID)abc group by DistrictName,DeseaseName ) fin,Districts where fin.DistrictName=Districts.DistrictName and Districts.DistrictID={2}", fromDate, toDate, DistrictId);

            string query = q1 + q2 + q3;

            string qyery1 = string.Format("select fin.DistrictName,NoOfPatient,Districts.DistrictID,Districts.population,(NoOfPatient/Districts.Population)*100 as pct,DeseaseName from (select DistrictName,COUNT(voternumber) as NoOfPatient,DeseaseName from (select DistrictName,ThanaName,ClinicName,voterNumber,DeseaseName from CommunityClinics,Districts,Deseases,Thanas,(SELECT * FROM Treatments WHERE date >= '{0}' AND date <  '{1}') a where a.DeseaseID=Deseases.DeseaseID and a.CenterID=CommunityClinics.CommunityClinicID and CommunityClinics.ThanaID=Thanas.ThanaID and Thanas.DistrictID=Districts.DistrictID)abc group by DistrictName,DeseaseName ) fin,Districts where fin.DistrictName=Districts.DistrictName and Districts.DistrictId={2} and  fin.DeseaseName='{3}'", fromDate, toDate, DistrictId, DiseaseName);
            string queryDistinctVoter =
                string.Format("select fin.DistrictName,NoOfPatient,Districts.DistrictID,Districts.population,(NoOfPatient/Districts.Population)*100 as pct,DeseaseName from (select DistrictName,COUNT(voternumber) as NoOfPatient,DeseaseName from (select DistrictName,ThanaName,ClinicName,voterNumber,DeseaseName from CommunityClinics,Districts,Deseases,Thanas,(SELECT * FROM Treatments WHERE date >= '{0}' AND date <  '{1}') a where a.DeseaseID=Deseases.DeseaseID and a.CenterID=CommunityClinics.CommunityClinicID and CommunityClinics.ThanaID=Thanas.ThanaID and Thanas.DistrictID=Districts.DistrictID)abc group by DistrictName,DeseaseName,voterNumber ) fin,Districts  where fin.DistrictName=Districts.DistrictName and Districts.DistrictId={2} and  fin.DeseaseName='{3}'",fromDate, toDate, DistrictId, DiseaseName);
            aSqlConnection.Open();
            aSqlCommand = new SqlCommand(qyery1, aSqlConnection);
            SqlDataReader aDataReader = aSqlCommand.ExecuteReader();

            List<DiseaseReportViewModel> aList = new List<DiseaseReportViewModel>();

            while (aDataReader.Read())
            {
                //Designation aDesignation = new Designation();
                DiseaseReportViewModel aDiseaseModel = new DiseaseReportViewModel();
                aDiseaseModel.DistrictName = aDataReader[0].ToString();
                aDiseaseModel.DistrictID = Convert.ToInt32(aDataReader[2]);
                aDiseaseModel.Population = Convert.ToInt32(aDataReader[3]);
                aDiseaseModel.NoOfPatient = Convert.ToInt32(aDataReader["NoOfPatient"]);
                aDiseaseModel.DeseaseName = Convert.ToString(aDataReader["DeseaseName"]);
                aList.Add(aDiseaseModel);
            }
            aDataReader.Close();
            aSqlConnection.Close();
            List<DiseaseReportViewModel> aList2 = new List<DiseaseReportViewModel>();
            
            {

                aSqlConnection.Open();
                aSqlCommand = new SqlCommand(queryDistinctVoter, aSqlConnection);
                SqlDataReader aDataReader1 = aSqlCommand.ExecuteReader();

                List<DiseaseReportViewModel> aList1 = new List<DiseaseReportViewModel>();

                while (aDataReader1.Read())
                {
                    
                    //Designation aDesignation = new Designation();
                    DiseaseReportViewModel aDiseaseModel = new DiseaseReportViewModel();
                    aDiseaseModel.DistrictName = aDataReader1[0].ToString();
                    aDiseaseModel.DistrictID = Convert.ToInt32(aDataReader1[2]);
                    aDiseaseModel.Population = Convert.ToInt32(aDataReader1[3]);
                    aDiseaseModel.NoOfPatient = Convert.ToInt32(aDataReader1["NoOfPatient"]);
                    aDiseaseModel.DeseaseName = Convert.ToString(aDataReader1["DeseaseName"]);
                    aList1.Add(aDiseaseModel);
                }
                
                DiseaseReportViewModel aDiseaseModel1 = new DiseaseReportViewModel();
                if (aList1.Count > 0)
                { 
                aDiseaseModel1.NoOfPatient = aList1.Count;
                aDiseaseModel1.DistrictName = aList1.First().DistrictName;
                aDiseaseModel1.DistrictID = aList1.First().DistrictID;
                aDiseaseModel1.Population = aList1.First().Population;
                aDiseaseModel1.Percentage = (aList1.Count/aList1.First().Population)*100;
                aDiseaseModel1.DeseaseName = aList1.First().DeseaseName;
                }
                aList2.Add(aDiseaseModel1);
                aDataReader1.Close();
                aSqlConnection.Close();
    

            }

            //if voter not distinct
            //return aList;

            //if voter distinct
            return aList2;
        }






        public void UpdateMedicineStock(int clinicId, int medicineId,int QtyGiven)
        {
            string qyery1 = string.Format("Update MedicineClinics Set Quantity=Quantity-{0} where CommunityClinicID={1} and Medicine_MedicineID={2}", QtyGiven, clinicId, medicineId);
            aSqlConnection.Open();
            aSqlCommand = new SqlCommand(qyery1, aSqlConnection);
            aSqlCommand.ExecuteNonQuery();
            
            aSqlConnection.Close();

        }

        public void UpdateMedicineDistributionClinic(int clinicId, int medicineId, int QtyGiven)
        {
            string qyery1 = string.Format("Update MedicineClinics Set Quantity=Quantity+{0} where CommunityClinicID={1} and Medicine_MedicineID={2}", QtyGiven, clinicId, medicineId);
            aSqlConnection.Open();
            aSqlCommand = new SqlCommand(qyery1, aSqlConnection);
            aSqlCommand.ExecuteNonQuery();

            aSqlConnection.Close();

        }

        public void UpdateMedicineHeadOffice(int medicineId, int QtyGiven)
        {
            string qyery1 = string.Format("Update Medicines Set Quantity=Quantity-{0} where MedicineID={1}", QtyGiven,medicineId);
            aSqlConnection.Open();
            aSqlCommand = new SqlCommand(qyery1, aSqlConnection);
            aSqlCommand.ExecuteNonQuery();

            aSqlConnection.Close();

        }

        public List<DeseaseWiseReport> GetDiseaseWiseData(string deseaseID, string myPicker1, string myPicker2)
        {
            //string query =select fin.DistrictName,NoOfPatient,Districts.DistrictID,Districts.population,(NoOfPatient/Districts.Population)*100 as pct,DeseaseName from (select DistrictName,COUNT(voternumber) as NoOfPatient,DeseaseName from (select DistrictName,ThanaName,ClinicName,voterNumber,DeseaseName from CommunityClinics,Districts,Deseases,Thanas,(SELECT * 
            //                FROM Treatments 
            //                    WHERE date >= '2012-10-01' 
            //                        AND date <  '2012-11-28') a where a.DeseaseID=Deseases.DeseaseID and a.CenterID=CommunityClinics.CommunityClinicID and CommunityClinics.ThanaID=Thanas.ThanaID and Thanas.DistrictID=Districts.DistrictID)abc group by DistrictName,DeseaseName ) fin,Districts where fin.DistrictName=Districts.DistrictName and fin.DeseaseName='Diarrhea'


            string q1 =
                "select fin.DistrictName,NoOfPatient,Districts.DistrictID,Districts.population,(NoOfPatient/Districts.Population)*100 as";

            string q2 =
                " pct,DeseaseName from (select DistrictName,COUNT(voternumber) as NoOfPatient,DeseaseName from (select DistrictName,ThanaName,ClinicName,voterNumber,DeseaseName from CommunityClinics,Districts,";

            string q3 =
                string.Format(
                    "Deseases,Thanas,(SELECT *FROM Treatments WHERE date >= '{1}' AND date <  '{2}') a where a.DeseaseID=Deseases.DeseaseID and a.CenterID=CommunityClinics.CommunityClinicID and CommunityClinics.ThanaID=Thanas.ThanaID and Thanas.DistrictID=Districts.DistrictID)abc group by DistrictName,DeseaseName ) fin,Districts where fin.DistrictName=Districts.DistrictName and fin.DeseaseName='{0}'",
                    deseaseID, myPicker1, myPicker2);


            string query2 = string.Format("select distinctCheck.DistrictName,COUNT(*) as NoOfPatient,DistrictID,distinctCheck.population,distinctCheck.pct,distinctCheck.DeseaseName from (select fin.DistrictName,NoOfPatient,Districts.DistrictID,Districts.population,(NoOfPatient/Districts.Population)*100 as pct,DeseaseName from (select DistrictName,COUNT(voternumber) as NoOfPatient,DeseaseName from (select DistrictName,ThanaName,ClinicName,voterNumber,DeseaseName from CommunityClinics,Districts,Deseases,Thanas,(SELECT * FROM Treatments WHERE date >= '{0}' AND date <  '{1}') a where a.DeseaseID=Deseases.DeseaseID and a.CenterID=CommunityClinics.CommunityClinicID and CommunityClinics.ThanaID=Thanas.ThanaID and Thanas.DistrictID=Districts.DistrictID)abc group by DistrictName,DeseaseName,voterNumber ) fin,Districts where fin.DistrictName=Districts.DistrictName and fin.DeseaseName='{2}') distinctCheck group by distinctCheck.DistrictName,DistrictID,distinctCheck.population,distinctCheck.pct,distinctCheck.DeseaseName", myPicker1, myPicker2, deseaseID);
            string query = q1 + q2 + q3;
            aSqlConnection.Open();
            aSqlCommand = new SqlCommand(query2, aSqlConnection);
            SqlDataReader aDataReader = aSqlCommand.ExecuteReader();

            List<DeseaseWiseReport> aList = new List<DeseaseWiseReport>();

            while (aDataReader.Read())
            {
                DeseaseWiseReport aDeseaseWiseReport = new DeseaseWiseReport();
                aDeseaseWiseReport.DistrictName = aDataReader["DistrictName"].ToString();
                aDeseaseWiseReport.TotalPatient = Convert.ToInt32(aDataReader["NoOfPatient"]);
                aDeseaseWiseReport.PercentagePopulation = Convert.ToDouble(aDataReader["pct"]);
                aList.Add(aDeseaseWiseReport);


            }
            aDataReader.Close();
            aSqlConnection.Close();
            return aList;
        }


        public List<Doctor> DoctorAssignList()
        {
            string query =
                "select DoctorID,DoctorName from doctors where doctorid not in (select DoctorID from DoctorClinics)";
            aSqlConnection.Open();
            aSqlCommand = new SqlCommand(query, aSqlConnection);
            SqlDataReader aDataReader = aSqlCommand.ExecuteReader();

            List<Doctor> aList = new List<Doctor>();

            while (aDataReader.Read())
            {
                //Designation aDesignation = new Designation();
                Doctor aDoctor = new Doctor();
                aDoctor.DoctorID = Convert.ToInt32(aDataReader[0]);
                aDoctor.DoctorName = aDataReader[1].ToString();

                aList.Add(aDoctor);
            }
            aDataReader.Close();
            aSqlConnection.Close();
            return aList;

        }



        public List<DiseaseReportViewModel> GetAllDemographicData(string fromDate, string toDate,string DiseaseName)
        {
            string query = string.Format("select distinctCheck.DistrictName,COUNT(*) as NoOfPatient,DistrictID,distinctCheck.population,distinctCheck.pct,distinctCheck.DeseaseName from (select fin.DistrictName,NoOfPatient,Districts.DistrictID,Districts.population,(NoOfPatient/Districts.Population)*100 as pct,DeseaseName from (select DistrictName,COUNT(voternumber) as NoOfPatient,DeseaseName from (select DistrictName,ThanaName,ClinicName,voterNumber,DeseaseName from CommunityClinics,Districts,Deseases,Thanas,(SELECT * FROM Treatments WHERE date >= '{0}' AND date <  '{1}') a where a.DeseaseID=Deseases.DeseaseID and a.CenterID=CommunityClinics.CommunityClinicID and CommunityClinics.ThanaID=Thanas.ThanaID and Thanas.DistrictID=Districts.DistrictID)abc group by DistrictName,DeseaseName,voterNumber ) fin,Districts where fin.DistrictName=Districts.DistrictName and fin.DeseaseName='{2}') distinctCheck group by distinctCheck.DistrictName,DistrictID,distinctCheck.population,distinctCheck.pct,distinctCheck.DeseaseName", fromDate, toDate, DiseaseName);
            
            aSqlConnection.Open();
            aSqlCommand = new SqlCommand(query, aSqlConnection);
            SqlDataReader aDataReader = aSqlCommand.ExecuteReader();

            List<DiseaseReportViewModel> aList = new List<DiseaseReportViewModel>();

            while (aDataReader.Read())
            {
                //Designation aDesignation = new Designation();
                DiseaseReportViewModel aDiseaseModel = new DiseaseReportViewModel();
                aDiseaseModel.DistrictName = aDataReader[0].ToString();
                aDiseaseModel.DistrictID = Convert.ToInt32(aDataReader[2]);
                aDiseaseModel.Population = Convert.ToInt32(aDataReader[3]);
                aDiseaseModel.NoOfPatient = Convert.ToInt32(aDataReader["NoOfPatient"]);
                aDiseaseModel.DeseaseName = Convert.ToString(aDataReader["DeseaseName"]);
                aList.Add(aDiseaseModel);
            }
            aDataReader.Close();
            aSqlConnection.Close();
            return aList;
        }






    }
}