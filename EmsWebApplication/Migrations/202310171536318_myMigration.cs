namespace EmsWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class myMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventName = c.String(),
                        EventDate = c.DateTime(nullable: false),
                        EventVenue = c.String(),
                        EventDescription = c.String(),
                        EventPrice = c.Int(nullable: false),
                        EventPromoDateStart = c.DateTime(nullable: false),
                        EventPromoDateEnd = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketId = c.Int(nullable: false, identity: true),
                        EventId = c.Int(),
                        UserId = c.String(),
                        PurchaseDate = c.DateTime(),
                        Quantity = c.Int(),
                        OriginalPrice = c.Decimal(precision: 18, scale: 2),
                        Discount = c.Decimal(precision: 18, scale: 2),
                        TotalPrice = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.TicketId)
                .ForeignKey("dbo.Events", t => t.EventId)
                .Index(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "EventId", "dbo.Events");
            DropIndex("dbo.Tickets", new[] { "EventId" });
            DropTable("dbo.Tickets");
            DropTable("dbo.Events");
        }
    }
}
