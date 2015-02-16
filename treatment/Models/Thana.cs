using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace treatment.Models
{
    public class Thana
    {
        public int ThanaID { set; get; }
        public int DistrictID { set; get; }
        public string ThanaName { set; get; }
        public virtual District District { set; get; }
        public virtual ICollection<CommunityClinic> CommunityClinics { set; get; } 
    }
}