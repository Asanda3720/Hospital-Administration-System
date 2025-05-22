namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookingValidations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bookings", "Message", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bookings", "Message", c => c.String());
        }
    }
}
