namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Pharmacist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pharmacists", "Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Pharmacists", "EmailAddress", c => c.String());
            AddColumn("dbo.Pharmacists", "Phone", c => c.String());
            AddColumn("dbo.Pharmacists", "Address", c => c.String());
            AddColumn("dbo.Pharmacists", "Province", c => c.String());
            AddColumn("dbo.Pharmacists", "City", c => c.String());
            AddColumn("dbo.Pharmacists", "Avatar", c => c.String());
            CreateIndex("dbo.Pharmacists", "Id");
            AddForeignKey("dbo.Pharmacists", "Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pharmacists", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Pharmacists", new[] { "Id" });
            DropColumn("dbo.Pharmacists", "Avatar");
            DropColumn("dbo.Pharmacists", "City");
            DropColumn("dbo.Pharmacists", "Province");
            DropColumn("dbo.Pharmacists", "Address");
            DropColumn("dbo.Pharmacists", "Phone");
            DropColumn("dbo.Pharmacists", "EmailAddress");
            DropColumn("dbo.Pharmacists", "Id");
        }
    }
}
