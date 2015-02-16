using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace treatment.Models
{
    public class Desease
    {
        public int DeseaseID { get; set; }
        [Required]
        [DisplayName("DeseaseName")]
        public string DeseaseName { get; set; }
        [Required]
        [DisplayName("TreatmentProcedure")]
        public string TreatmentProcedure { get; set; }
        [Required]
        [DisplayName("Prefered Medicines")]
        public string PreferedMedicines { get; set; }
        public virtual ICollection<Treatment> Treatments { set; get; }
    }
}