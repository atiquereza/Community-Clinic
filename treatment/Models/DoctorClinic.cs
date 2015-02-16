using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace treatment.Models
{
    public class DoctorClinic
    {
        public int ID { get; set; }
        public int DoctorID { set; get; }
        public int CommunityClinicID { set; get; }
        public virtual Doctor Doctor { set; get; }
        public virtual CommunityClinic CommunityClinic { set; get; }
    }
}