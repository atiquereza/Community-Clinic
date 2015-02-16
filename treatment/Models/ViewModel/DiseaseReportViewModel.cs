using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace treatment.Models.ViewModel
{
    public class DiseaseReportViewModel
    {
        public int ID { set; get; }
        public string DistrictName { set; get; }
        public int NoOfPatient { set; get; }
        public int DistrictID { set; get; }
        public int Population { set; get; }
        public double Percentage { set; get; }
        public string DeseaseName { set; get; }
    }
}