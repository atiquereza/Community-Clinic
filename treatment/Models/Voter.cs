using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.Expressions;

namespace treatment.Models
{
    public class Voter
    {
        public int VoterID { get; set; }
        public string VoterNumber { set; get; }
    
        public string Name { set; get; }
        public string Address { get; set; }
        public string DateOFBirth { set; get; }
        
    }
}