namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompleteStandardBookingsAddStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "Status", c => c.String());
            AddColumn("dbo.StandardBookings", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.StandardBookings", "Status");
            DropColumn("dbo.Bookings", "Status");
        }
    }
}
