namespace EmsWebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Application_Migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupSize = c.Int(),
                        DiscountPercentage = c.Int(),
                        Coupon = c.String(),
                        PromotionDiscount = c.Int(),
                        Total = c.Int(),
                        TotalAfterDiscount = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Groups");
        }
    }
}
