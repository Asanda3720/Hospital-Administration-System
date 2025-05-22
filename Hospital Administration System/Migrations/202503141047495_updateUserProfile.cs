namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateUserProfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "Province", c => c.String());
            DropColumn("dbo.UserProfiles", "ZipCode");
            DropColumn("dbo.UserProfiles", "Country");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfiles", "Country", c => c.String());
            AddColumn("dbo.UserProfiles", "ZipCode", c => c.String());
            DropColumn("dbo.UserProfiles", "Province");
        }
    }
}
