namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmergencyRequests : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmergencyRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.Int(nullable: false),
                        PhoneNumber = c.Int(nullable: false),
                        Latitude = c.String(),
                        Longitude = c.String(),
                        IsAttended = c.Boolean(nullable: false),
                        IsNotified = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EmergencyRequests");
        }
    }
}
