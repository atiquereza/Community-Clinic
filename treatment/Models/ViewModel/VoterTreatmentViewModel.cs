using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace treatment.Models.ViewModel
{
    public class VoterTreatmentViewModel
    {

        public int VoterID { get; set; }
        public string VoterNumber { set; get; }

        public string Name { set; get; }
        public string Address { get; set; }
        public string DateOFBirth { set; get; }
        public int Treatmentcount { set; get; }

    }
}