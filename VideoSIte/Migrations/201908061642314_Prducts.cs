namespace VideoSIte.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Prducts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductCategory",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        ProductCatId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.ProductCatId })
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.ProductCatId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.ProductCatId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductCategory", "ProductCatId", "dbo.Categories");
            DropForeignKey("dbo.ProductCategory", "ProductId", "dbo.Products");
            DropIndex("dbo.ProductCategory", new[] { "ProductCatId" });
            DropIndex("dbo.ProductCategory", new[] { "ProductId" });
            DropTable("dbo.ProductCategory");
            DropTable("dbo.Products");
        }
    }
}
