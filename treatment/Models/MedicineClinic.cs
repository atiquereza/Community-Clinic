using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace treatment.Models
{
    public class MedicineClinic
    {
        public int MedicineClinicID { set; get; }
        public int Quantity { set; get; }
        public int CommunityClinicID { set; get; }
        public int Medicine_MedicineID { set; get; }
        public virtual Medicine Medicine { set; get; }
        public virtual CommunityClinic CommunityClinic { set; get; }
    }
}