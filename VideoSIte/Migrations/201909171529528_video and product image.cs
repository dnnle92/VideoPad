namespace VideoSIte.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class videoandproductimage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ProductImage", c => c.String());
            AddColumn("dbo.Videos", "VideoImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Videos", "VideoImage");
            DropColumn("dbo.Products", "ProductImage");
        }
    }
}
