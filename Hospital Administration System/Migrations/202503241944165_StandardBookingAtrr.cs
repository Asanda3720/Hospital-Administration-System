namespace phamacy_management_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StandardBookingAtrr : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StandardBookings",
                c => new
                    {
                        StandardBookingId = c.Int(nullable: false, identity: true),
                        RoomId = c.Int(),
                        FullName = c.String(),
                        Email = c.String(),
                        PhoneNo = c.String(),
                        AssignedDoctorId = c.Int(),
                        AppointmentID = c.String(),
                        BookingDate = c.DateTime(nullable: false),
                        BookingTime = c.DateTime(nullable: false),
                        Message = c.String(nullable: false),
                        BookCategoryId = c.Int(),
                        Paid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.StandardBookingId)
                .ForeignKey("dbo.BookCategories", t => t.BookCategoryId)
                .ForeignKey("dbo.Rooms", t => t.RoomId)
                .Index(t => t.RoomId)
                .Index(t => t.BookCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StandardBookings", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.StandardBookings", "BookCategoryId", "dbo.BookCategories");
            DropIndex("dbo.StandardBookings", new[] { "BookCategoryId" });
            DropIndex("dbo.StandardBookings", new[] { "RoomId" });
            DropTable("dbo.StandardBookings");
        }
    }
}
