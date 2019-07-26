namespace VideoSIte.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserDetail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserDetails",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        City = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserDetails", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserDetails", new[] { "Id" });
            DropTable("dbo.UserDetails");
        }
    }
}
