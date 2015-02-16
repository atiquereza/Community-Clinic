using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace treatment.Models
{
    public class DistrictThanaDetails
    {
        public int DistrictID { set; get; }
        public string DistrictName { get; set; }
        public int ThanaID { set; get; }
        public string ThanaName { set; get; }
        public int CommunityClinicID { set; get; }
        public string ClinicName { set; get; }


    }
}