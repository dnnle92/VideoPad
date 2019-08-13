namespace VideoSIte.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class videoUrlId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Videos", "VideoUrlId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Videos", "VideoUrlId");
        }
    }
}
