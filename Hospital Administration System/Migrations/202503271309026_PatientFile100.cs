namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PatientFile100 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PatientFiles", "PatientFileReference", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PatientFiles", "PatientFileReference");
        }
    }
}
