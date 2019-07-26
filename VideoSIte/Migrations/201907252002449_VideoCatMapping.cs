namespace VideoSIte.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VideoCatMapping : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VideoCategories",
                c => new
                    {
                        CatId = c.Int(nullable: false, identity: true),
                        CatName = c.String(),
                        Parent_CatId = c.Int(),
                    })
                .PrimaryKey(t => t.CatId)
                .ForeignKey("dbo.VideoCategories", t => t.Parent_CatId)
                .Index(t => t.Parent_CatId);
            
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        VideoId = c.Int(nullable: false, identity: true),
                        VideoName = c.String(),
                    })
                .PrimaryKey(t => t.VideoId);
            
            CreateTable(
                "dbo.VideoCategoryMapping",
                c => new
                    {
                        VideoId = c.Int(nullable: false),
                        VideoCatId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.VideoId, t.VideoCatId })
                .ForeignKey("dbo.Videos", t => t.VideoId, cascadeDelete: true)
                .ForeignKey("dbo.VideoCategories", t => t.VideoCatId, cascadeDelete: true)
                .Index(t => t.VideoId)
                .Index(t => t.VideoCatId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VideoCategoryMapping", "VideoCatId", "dbo.VideoCategories");
            DropForeignKey("dbo.VideoCategoryMapping", "VideoId", "dbo.Videos");
            DropForeignKey("dbo.VideoCategories", "Parent_CatId", "dbo.VideoCategories");
            DropIndex("dbo.VideoCategoryMapping", new[] { "VideoCatId" });
            DropIndex("dbo.VideoCategoryMapping", new[] { "VideoId" });
            DropIndex("dbo.VideoCategories", new[] { "Parent_CatId" });
            DropTable("dbo.VideoCategoryMapping");
            DropTable("dbo.Videos");
            DropTable("dbo.VideoCategories");
        }
    }
}
