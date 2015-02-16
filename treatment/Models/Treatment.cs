using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Cors;
namespace treatment.Models
{
    public class Treatment
    {
        public int TreatmentID { get; set; }
        public int CenterID { set; get; }
        public int DoctorID { set; get; }
        public int DeseaseID { set; get; }
        public string VoterNumber { set; get; }
        public string Observation { set; get; }
        public DateTime Date { set; get; }
        public virtual Doctor Doctor { set; get; }
        public virtual CommunityClinic CommunityClinic { set; get; }

        public virtual Desease Desease { set; get; }
        public virtual ICollection<TreatmentMedicine> TreatmentMedicines { set; get; }

    }
}