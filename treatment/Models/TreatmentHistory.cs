using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace treatment.Models
{
    public class TreatmentHistory
    {
        public string ClinicName { get; set; }
        public string DeseaseName { get; set; }
        public string Observation { set; get; }
        public string Date { set; get; }
        public string VoterNumber { set; get; }
    }
}