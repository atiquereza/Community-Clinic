namespace treatment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TreatmentMedicines", "MedicineId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TreatmentMedicines", "MedicineId");
        }
    }
}
