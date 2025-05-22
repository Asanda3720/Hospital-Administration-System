namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrescriptionAddIsDispensed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prescriptions", "IsDispensed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Prescriptions", "IsDispensed");
        }
    }
}
