using System.Collections.Generic;
using treatment.Models;

namespace treatment.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<treatment.Models.CommunityDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(treatment.Models.CommunityDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //var treatmentList = new List<Treatment> {
            //        new Treatment() {CenterID = 2,DoctorID = 2,Date = DateTime.Parse("2012-09-01"),VoterNumber="5644309456813",Observation = "Fever",DeseaseID = 1},
            //         new Treatment() {CenterID = 3,DoctorID = 1,Date = DateTime.Parse("2012-10-01"),VoterNumber="9509623450915",Observation = "Cold",DeseaseID = 1},
            //          new Treatment() {CenterID = 2,DoctorID = 2,Date = DateTime.Parse("2012-10-05"),VoterNumber="1098789543218",Observation = "Malnutrition",DeseaseID = 2},
            //          new Treatment() {CenterID = 3,DoctorID = 2,Date = DateTime.Parse("2012-10-06"),VoterNumber="1098789543218",Observation = "Chest Pain",DeseaseID = 2}



            //    };

            //treatmentList.ForEach(s => context.Treatments.AddOrUpdate(s));
            //context.SaveChanges();

            //var accountList = new List<Account> {
            //        new Account() {UserName = "head101",Password = "head101",UserRole= "Head"},
            //         new Account() {CommunityClinicID = 3,UserName = "bbb101",Password = "bbb101",UserRole= "Clinic"},
            //          new Account() {CommunityClinicID = 4,UserName = "kafrul1101",Password = "kafrul1101",UserRole= "Clinic"},
            //          new Account() {CommunityClinicID = 7,UserName = "Raj01",Password = "Raj01101",UserRole= "Clinic"},



            //    };

            //accountList.ForEach(s => context.Accounts.AddOrUpdate(s));
            //context.SaveChanges();


            //var treatmentMed = new List<TreatmentMedicine> {
            //        new TreatmentMedicine() {TreatmentID = 2,Medicine = "Napa",Dose= "0-1-1",QtyGiven = 10,Note = "not so bad",B4AfterMeal = "Before Meal"},
            //         new TreatmentMedicine() {TreatmentID = 3,Medicine = "Napa",Dose= "0-1-1",QtyGiven = 10,Note = "not so bad",B4AfterMeal = "After Meal"},
            //          new TreatmentMedicine() {TreatmentID = 4,Medicine = "Orsalaine",Dose= "0-1-1",QtyGiven = 2,Note = "Unaware",B4AfterMeal = "After Meal"},
            //          new TreatmentMedicine() {TreatmentID = 4,Medicine = "Antacid",Dose= "1-1-1",QtyGiven = 10,Note = "not so bad",B4AfterMeal = "Before Meal"}



            //    };

            //treatmentMed.ForEach(s => context.TreatmentMedicines.AddOrUpdate(s));
            //context.SaveChanges();    



        }
    }

}