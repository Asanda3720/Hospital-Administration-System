namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MedicationValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Medications", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Medications", "Quantity", c => c.String(nullable: false));
            AlterColumn("dbo.Medications", "Type", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Medications", "Type", c => c.String());
            AlterColumn("dbo.Medications", "Quantity", c => c.String());
            AlterColumn("dbo.Medications", "Name", c => c.String());
        }
    }
}
