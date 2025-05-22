namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageFile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "Avatar", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserProfiles", "Avatar");
        }
    }
}
