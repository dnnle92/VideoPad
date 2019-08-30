namespace VideoSIte.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductVideo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductVideo",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        ProductVidId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.ProductVidId })
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Videos", t => t.ProductVidId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.ProductVidId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductVideo", "ProductVidId", "dbo.Videos");
            DropForeignKey("dbo.ProductVideo", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductVideo", new[] { "ProductVidId" });
            DropIndex("dbo.ProductVideo", new[] { "ProductId" });
            DropTable("dbo.ProductVideo");
        }
    }
}
