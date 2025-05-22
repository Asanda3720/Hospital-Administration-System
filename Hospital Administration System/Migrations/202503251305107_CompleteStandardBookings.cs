namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompleteStandardBookings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StandardBookings", "Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.StandardBookings", "Id");
            AddForeignKey("dbo.StandardBookings", "Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StandardBookings", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.StandardBookings", new[] { "Id" });
            DropColumn("dbo.StandardBookings", "Id");
        }
    }
}
