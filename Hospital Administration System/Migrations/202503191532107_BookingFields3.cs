namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookingFields3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "PatientEmail", c => c.String());
            AddColumn("dbo.Bookings", "PatientPhone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bookings", "PatientPhone");
            DropColumn("dbo.Bookings", "PatientEmail");
        }
    }
}
