namespace VideoSIte.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productsdateadded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "DateAdded", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "DateAdded");
        }
    }
}
