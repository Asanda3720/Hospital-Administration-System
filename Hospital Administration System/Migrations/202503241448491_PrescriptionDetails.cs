namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrescriptionDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PrescriptionDetails",
                c => new
                    {
                        PrescriptionDetailsId = c.Int(nullable: false, identity: true),
                        MedicationId = c.Int(nullable: false),
                        Purpose = c.String(nullable: false),
                        Dosage = c.String(nullable: false),
                        Route = c.String(nullable: false),
                        Frequently = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PrescriptionDetailsId)
                .ForeignKey("dbo.Medications", t => t.MedicationId, cascadeDelete: true)
                .Index(t => t.MedicationId);
            
            AddColumn("dbo.Prescriptions", "PrescriptionCode", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PrescriptionDetails", "MedicationId", "dbo.Medications");
            DropIndex("dbo.PrescriptionDetails", new[] { "MedicationId" });
            DropColumn("dbo.Prescriptions", "PrescriptionCode");
            DropTable("dbo.PrescriptionDetails");
        }
    }
}
