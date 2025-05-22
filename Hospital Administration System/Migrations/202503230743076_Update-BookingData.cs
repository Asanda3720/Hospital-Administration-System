namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBookingData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        RoomNumber = c.String(),
                        Capacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoomId);
            
            AddColumn("dbo.Bookings", "RoomId", c => c.Int());
            AddColumn("dbo.Bookings", "FullName", c => c.String());
            AddColumn("dbo.Bookings", "Email", c => c.String());
            AddColumn("dbo.Bookings", "PhoneNo", c => c.String());
            AddColumn("dbo.Bookings", "BookingDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Bookings", "BookingTime", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Bookings", "RoomId");
            AddForeignKey("dbo.Bookings", "RoomId", "dbo.Rooms", "RoomId");
            DropColumn("dbo.Bookings", "RoomNumber");
            DropColumn("dbo.Bookings", "PatientName");
            DropColumn("dbo.Bookings", "PatientEmail");
            DropColumn("dbo.Bookings", "PatientPhone");
            DropColumn("dbo.Bookings", "BookDate");
            DropColumn("dbo.Bookings", "BookTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "BookTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Bookings", "BookDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Bookings", "PatientPhone", c => c.String());
            AddColumn("dbo.Bookings", "PatientEmail", c => c.String());
            AddColumn("dbo.Bookings", "PatientName", c => c.String());
            AddColumn("dbo.Bookings", "RoomNumber", c => c.String());
            DropForeignKey("dbo.Bookings", "RoomId", "dbo.Rooms");
            DropIndex("dbo.Bookings", new[] { "RoomId" });
            DropColumn("dbo.Bookings", "BookingTime");
            DropColumn("dbo.Bookings", "BookingDate");
            DropColumn("dbo.Bookings", "PhoneNo");
            DropColumn("dbo.Bookings", "Email");
            DropColumn("dbo.Bookings", "FullName");
            DropColumn("dbo.Bookings", "RoomId");
            DropTable("dbo.Rooms");
        }
    }
}
