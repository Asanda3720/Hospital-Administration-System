namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmergencyRequests2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EmergencyRequests", "FullName", c => c.String(nullable: false));
            AlterColumn("dbo.EmergencyRequests", "PhoneNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EmergencyRequests", "PhoneNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.EmergencyRequests", "FullName", c => c.Int(nullable: false));
        }
    }
}
