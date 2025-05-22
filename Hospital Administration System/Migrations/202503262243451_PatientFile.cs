namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PatientFile : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PatientFiles",
                c => new
                    {
                        PatientFileId = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(nullable: false),
                        IDNumber = c.String(),
                        Age = c.String(),
                        Gender = c.String(),
                        NextOfKinName = c.String(),
                        NextOfKinPhone = c.String(),
                        NextOfKinRelationship = c.String(),
                        Allergies = c.String(),
                        DateOfBirth = c.String(),
                        Height = c.Double(nullable: false),
                        Weight = c.Double(nullable: false),
                        ChronicDisease = c.Boolean(nullable: false),
                        UnderCare = c.Boolean(nullable: false),
                        DrugAllergies = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PatientFileId)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PatientFiles", "PatientId", "dbo.Patients");
            DropIndex("dbo.PatientFiles", new[] { "PatientId" });
            DropTable("dbo.PatientFiles");
        }
    }
}
