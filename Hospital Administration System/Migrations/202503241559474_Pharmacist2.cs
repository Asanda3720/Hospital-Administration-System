namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pharmacist2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pharmacists", "ProfileId", "dbo.UserProfiles");
            DropIndex("dbo.Pharmacists", new[] { "ProfileId" });
            DropColumn("dbo.Pharmacists", "ProfileId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pharmacists", "ProfileId", c => c.Int());
            CreateIndex("dbo.Pharmacists", "ProfileId");
            AddForeignKey("dbo.Pharmacists", "ProfileId", "dbo.UserProfiles", "ProfileId");
        }
    }
}
