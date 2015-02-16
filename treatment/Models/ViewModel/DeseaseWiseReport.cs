using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace treatment.Models.ViewModel
{
    public class DeseaseWiseReport
    {

        public string DistrictName { get; set; }
        public int TotalPatient { get; set; }
        public double PercentagePopulation { get; set; }
    }
}