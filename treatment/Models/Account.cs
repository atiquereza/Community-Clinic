using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace treatment.Models
{
    public class Account
    {
        public int AccountID { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string UserRole { get; set; }
        public int CommunityClinicID { get; set; }
        //public virtual CommunityClinic CommunityClinic { set; get; }
    }
}