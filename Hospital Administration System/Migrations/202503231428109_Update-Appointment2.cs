namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAppointment2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Appointments", "IsAttended", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Appointments", "IsAttended");
        }
    }
}
