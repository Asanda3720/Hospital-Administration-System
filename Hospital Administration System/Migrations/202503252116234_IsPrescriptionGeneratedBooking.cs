namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsPrescriptionGeneratedBooking : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "IsPrescriptionGenerated", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bookings", "IsPrescriptionGenerated");
        }
    }
}
