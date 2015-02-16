using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace treatment.Models
{
    public class CommunityClinic
    {
        public int CommunityClinicID { set; get; }
        
        [Required]
        public int ThanaID { set; get; }
        [Required]
        public string ClinicName { set; get; }
        
       
        
        public virtual Thana Thana { set; get; }
        
        public virtual ICollection<DoctorClinic> DoctorClinics { set; get; }
        public virtual ICollection<Treatment> Treatments { set; get; }
        public virtual ICollection<MedicineClinic> MedicineClinics { set; get; }
        //public virtual ICollection<Account> Accounts { set; get; }
    }
}