namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoctorFields : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Doctors", "ProfileId", "dbo.UserProfiles");
            DropIndex("dbo.Doctors", new[] { "ProfileId" });
            AddColumn("dbo.Doctors", "Title", c => c.String());
            AddColumn("dbo.Doctors", "Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Doctors", "FirstName", c => c.String());
            AddColumn("dbo.Doctors", "LastName", c => c.String());
            AddColumn("dbo.Doctors", "City", c => c.String());
            AddColumn("dbo.Doctors", "Phone", c => c.String());
            AddColumn("dbo.Doctors", "Avatar", c => c.String());
            CreateIndex("dbo.Doctors", "Id");
            AddForeignKey("dbo.Doctors", "Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Doctors", "ProfileId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Doctors", "ProfileId", c => c.Int());
            DropForeignKey("dbo.Doctors", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Doctors", new[] { "Id" });
            DropColumn("dbo.Doctors", "Avatar");
            DropColumn("dbo.Doctors", "Phone");
            DropColumn("dbo.Doctors", "City");
            DropColumn("dbo.Doctors", "LastName");
            DropColumn("dbo.Doctors", "FirstName");
            DropColumn("dbo.Doctors", "Id");
            DropColumn("dbo.Doctors", "Title");
            CreateIndex("dbo.Doctors", "ProfileId");
            AddForeignKey("dbo.Doctors", "ProfileId", "dbo.UserProfiles", "ProfileId");
        }
    }
}
