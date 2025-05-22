namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookingFields2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "PatientName", c => c.String());
            AddColumn("dbo.Bookings", "AppointmentID", c => c.String());
            AddColumn("dbo.Bookings", "DoctorId", c => c.Int());
            AddColumn("dbo.Bookings", "BookDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Bookings", "BookTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Bookings", "Message", c => c.String());
            CreateIndex("dbo.Bookings", "DoctorId");
            AddForeignKey("dbo.Bookings", "DoctorId", "dbo.Doctors", "DoctorId");
            DropColumn("dbo.Bookings", "BookiTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "BookiTime", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Bookings", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.Bookings", new[] { "DoctorId" });
            DropColumn("dbo.Bookings", "Message");
            DropColumn("dbo.Bookings", "BookTime");
            DropColumn("dbo.Bookings", "BookDate");
            DropColumn("dbo.Bookings", "DoctorId");
            DropColumn("dbo.Bookings", "AppointmentID");
            DropColumn("dbo.Bookings", "PatientName");
        }
    }
}
