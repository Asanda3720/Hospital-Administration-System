namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookCategories",
                c => new
                    {
                        BookCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.BookCategoryId);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(),
                        RoomNumber = c.String(),
                        BookiTime = c.DateTime(nullable: false),
                        BookCategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.BookCategories", t => t.BookCategoryId)
                .ForeignKey("dbo.Patients", t => t.PatientId)
                .Index(t => t.PatientId)
                .Index(t => t.BookCategoryId);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientId = c.Int(nullable: false, identity: true),
                        ProfileId = c.Int(),
                        Gender = c.String(),
                        NextOfKinNumber = c.String(),
                        NextOfKinName = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PatientId)
                .ForeignKey("dbo.UserProfiles", t => t.ProfileId)
                .Index(t => t.ProfileId);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DoctorId = c.Int(nullable: false, identity: true),
                        DoctorType = c.String(),
                        ProfileId = c.Int(),
                    })
                .PrimaryKey(t => t.DoctorId)
                .ForeignKey("dbo.UserProfiles", t => t.ProfileId)
                .Index(t => t.ProfileId);
            
            CreateTable(
                "dbo.Labs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LabName = c.String(),
                        LabTest = c.String(),
                        DoctorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doctors", t => t.DoctorId)
                .Index(t => t.DoctorId);
            
            CreateTable(
                "dbo.Medications",
                c => new
                    {
                        MedicationId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Quantity = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.MedicationId);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        PaymentId = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(),
                        CardNumber = c.String(),
                        CVV = c.String(),
                        ExpiryDate = c.DateTime(nullable: false),
                        CardHolder = c.String(),
                    })
                .PrimaryKey(t => t.PaymentId)
                .ForeignKey("dbo.Patients", t => t.PatientId)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.Pharmacists",
                c => new
                    {
                        PharmacistId = c.Int(nullable: false, identity: true),
                        PharmacistName = c.String(),
                    })
                .PrimaryKey(t => t.PharmacistId);
            
            CreateTable(
                "dbo.Prescriptions",
                c => new
                    {
                        PrescriptionId = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(),
                        DoctorId = c.Int(),
                        PharmacistId = c.Int(),
                        PrescriptionDetails = c.String(),
                    })
                .PrimaryKey(t => t.PrescriptionId)
                .ForeignKey("dbo.Doctors", t => t.DoctorId)
                .ForeignKey("dbo.Patients", t => t.PatientId)
                .ForeignKey("dbo.Pharmacists", t => t.PharmacistId)
                .Index(t => t.PatientId)
                .Index(t => t.DoctorId)
                .Index(t => t.PharmacistId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prescriptions", "PharmacistId", "dbo.Pharmacists");
            DropForeignKey("dbo.Prescriptions", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Prescriptions", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Payments", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Labs", "DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Doctors", "ProfileId", "dbo.UserProfiles");
            DropForeignKey("dbo.Bookings", "PatientId", "dbo.Patients");
            DropForeignKey("dbo.Patients", "ProfileId", "dbo.UserProfiles");
            DropForeignKey("dbo.Bookings", "BookCategoryId", "dbo.BookCategories");
            DropIndex("dbo.Prescriptions", new[] { "PharmacistId" });
            DropIndex("dbo.Prescriptions", new[] { "DoctorId" });
            DropIndex("dbo.Prescriptions", new[] { "PatientId" });
            DropIndex("dbo.Payments", new[] { "PatientId" });
            DropIndex("dbo.Labs", new[] { "DoctorId" });
            DropIndex("dbo.Doctors", new[] { "ProfileId" });
            DropIndex("dbo.Patients", new[] { "ProfileId" });
            DropIndex("dbo.Bookings", new[] { "BookCategoryId" });
            DropIndex("dbo.Bookings", new[] { "PatientId" });
            DropTable("dbo.Prescriptions");
            DropTable("dbo.Pharmacists");
            DropTable("dbo.Payments");
            DropTable("dbo.Medications");
            DropTable("dbo.Labs");
            DropTable("dbo.Doctors");
            DropTable("dbo.Patients");
            DropTable("dbo.Bookings");
            DropTable("dbo.BookCategories");
        }
    }
}
