using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace treatment.Models
{
    public class Medicine
    {
        [Key]
        public int MedicineID { set; get; }
        [Required]
        [DisplayName("Medicine Name")]
        public string MedicineName { set; get; }
      
        [DisplayName("Weight Containts")]
        public string WeightContaints { set; get; }
        [Required]
         [DisplayName("MG/ML")]
       
        public string MG_ML { set; get; }
        [Required]
        [DisplayName("Medicine Quantity")]
        public int Quantity { set; get; }
        public virtual ICollection<MedicineClinic> MedicineClinics { set; get; }
    }
}