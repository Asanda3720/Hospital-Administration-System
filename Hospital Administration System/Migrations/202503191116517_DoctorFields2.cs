namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoctorFields2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doctors", "EmailAddress", c => c.String());
            AddColumn("dbo.Doctors", "Address", c => c.String());
            AddColumn("dbo.Doctors", "Province", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doctors", "Province");
            DropColumn("dbo.Doctors", "Address");
            DropColumn("dbo.Doctors", "EmailAddress");
        }
    }
}
