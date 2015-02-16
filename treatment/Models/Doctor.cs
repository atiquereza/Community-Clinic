using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace treatment.Models
{
    public class Doctor
    {
        public int DoctorID { set; get; }
        public string DoctorName { set; get; }
        public string Designation { set; get; }
        public string Degree { set; get; }
        public string Specialization { set; get; }
     
        public virtual ICollection<DoctorClinic> DoctorClinics { set; get; }
        public virtual ICollection<Treatment> Treatments { set; get; }
    }
}