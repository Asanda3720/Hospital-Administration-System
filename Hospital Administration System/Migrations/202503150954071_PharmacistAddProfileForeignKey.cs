namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PharmacistAddProfileForeignKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pharmacists", "ProfileId", c => c.Int());
            CreateIndex("dbo.Pharmacists", "ProfileId");
            AddForeignKey("dbo.Pharmacists", "ProfileId", "dbo.UserProfiles", "ProfileId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pharmacists", "ProfileId", "dbo.UserProfiles");
            DropIndex("dbo.Pharmacists", new[] { "ProfileId" });
            DropColumn("dbo.Pharmacists", "ProfileId");
        }
    }
}
