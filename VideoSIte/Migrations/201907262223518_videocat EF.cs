namespace VideoSIte.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class videocatEF : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CatId = c.Int(nullable: false, identity: true),
                        CatName = c.String(),
                        Parent_CatId = c.Int(),
                    })
                .PrimaryKey(t => t.CatId)
                .ForeignKey("dbo.Categories", t => t.Parent_CatId)
                .Index(t => t.Parent_CatId);
            
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        VideoId = c.Int(nullable: false, identity: true),
                        VideoName = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.VideoId);
            
            CreateTable(
                "dbo.VideoCategory",
                c => new
                    {
                        VideoId = c.Int(nullable: false),
                        VideoCatId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.VideoId, t.VideoCatId })
                .ForeignKey("dbo.Videos", t => t.VideoId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.VideoCatId, cascadeDelete: true)
                .Index(t => t.VideoId)
                .Index(t => t.VideoCatId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VideoCategory", "VideoCatId", "dbo.Categories");
            DropForeignKey("dbo.VideoCategory", "VideoId", "dbo.Videos");
            DropForeignKey("dbo.Categories", "Parent_CatId", "dbo.Categories");
            DropIndex("dbo.VideoCategory", new[] { "VideoCatId" });
            DropIndex("dbo.VideoCategory", new[] { "VideoId" });
            DropIndex("dbo.Categories", new[] { "Parent_CatId" });
            DropTable("dbo.VideoCategory");
            DropTable("dbo.Videos");
            DropTable("dbo.Categories");
        }
    }
}
