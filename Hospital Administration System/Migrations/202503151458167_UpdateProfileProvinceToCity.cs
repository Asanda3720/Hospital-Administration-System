namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProfileProvinceToCity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfiles", "City", c => c.String());
            DropColumn("dbo.UserProfiles", "Province");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfiles", "Province", c => c.String());
            DropColumn("dbo.UserProfiles", "City");
        }
    }
}
