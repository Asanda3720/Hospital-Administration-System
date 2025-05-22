namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LabTestBookingAndTechnicians : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LabTestBookings",
                c => new
                    {
                        LabTestBookingId = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(nullable: false),
                        ReasonForBooking = c.String(),
                        Date = c.DateTime(nullable: false),
                        Time = c.DateTime(nullable: false),
                        Additional = c.String(),
                    })
                .PrimaryKey(t => t.LabTestBookingId)
                .ForeignKey("dbo.Patients", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.Technicians",
                c => new
                    {
                        TechnicianId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Id = c.String(maxLength: 128),
                        IsAvailable = c.Boolean(nullable: false),
                        AvailabilityStatus = c.String(),
                    })
                .PrimaryKey(t => t.TechnicianId)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Technicians", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.LabTestBookings", "PatientId", "dbo.Patients");
            DropIndex("dbo.Technicians", new[] { "Id" });
            DropIndex("dbo.LabTestBookings", new[] { "PatientId" });
            DropTable("dbo.Technicians");
            DropTable("dbo.LabTestBookings");
        }
    }
}
