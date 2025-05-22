namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmergencyRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmergencyRequests", "DoctorId", c => c.Int());
            CreateIndex("dbo.EmergencyRequests", "DoctorId");
            AddForeignKey("dbo.EmergencyRequests", "DoctorId", "dbo.Doctors", "DoctorId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmergencyRequests", "DoctorId", "dbo.Doctors");
            DropIndex("dbo.EmergencyRequests", new[] { "DoctorId" });
            DropColumn("dbo.EmergencyRequests", "DoctorId");
        }
    }
}
