namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StandardBookingAddedField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "BookingType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bookings", "BookingType");
        }
    }
}
