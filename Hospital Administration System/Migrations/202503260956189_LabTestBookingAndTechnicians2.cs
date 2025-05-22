namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LabTestBookingAndTechnicians2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LabTestBookings", "IsResultsPresent", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LabTestBookings", "IsResultsPresent");
        }
    }
}
