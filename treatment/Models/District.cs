using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace treatment.Models
{
    public class District
    {
        public int DistrictID { set; get; }
        public string DistrictName { set; get; }
        public int Population { set; get; }
        public virtual ICollection<Thana> Thanas { set; get; }
        //public virtual ICollection<CommunityClinic> CommunityClinics { set; get; } 
    }
}