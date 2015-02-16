using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace treatment.Models
{
    

    public class CommunityDBContext : DbContext
    {
        public CommunityDBContext(): base("CommunityMedicineDBContext")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public System.Data.Entity.DbSet<treatment.Models.District> Districts { get; set; }

        public System.Data.Entity.DbSet<treatment.Models.Thana> Thanas { get; set; }

        public System.Data.Entity.DbSet<treatment.Models.CommunityClinic> CommunityClinics { get; set; }

        public System.Data.Entity.DbSet<treatment.Models.Medicine> Medicines { get; set; }

        public System.Data.Entity.DbSet<treatment.Models.MedicineClinic> MedicineClinics { get; set; }

        public System.Data.Entity.DbSet<treatment.Models.Treatment> Treatments { get; set; }

        public System.Data.Entity.DbSet<treatment.Models.Doctor> Doctors { get; set; }

        public System.Data.Entity.DbSet<treatment.Models.Voter> Voters { get; set; }
        public System.Data.Entity.DbSet<treatment.Models.MedicineDose> MedicineDoses { get; set; }

        public System.Data.Entity.DbSet<treatment.Models.Desease> Deseases { get; set; }
        public System.Data.Entity.DbSet<treatment.Models.Account> Accounts { get; set; }
        public System.Data.Entity.DbSet<treatment.Models.TreatmentMedicine> TreatmentMedicines { get; set; }

        public System.Data.Entity.DbSet<treatment.Models.ViewModel.MedicineStockViewModel> MedicineStockViewModels { get; set; }
        public System.Data.Entity.DbSet<treatment.Models.DoctorClinic> DoctorClinics { get; set; }
      
    }



}