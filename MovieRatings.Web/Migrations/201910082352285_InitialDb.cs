namespace MovieRatings.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movie",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(unicode: false),
                        ReleaseDate = c.DateTime(nullable: false, precision: 0),
                        Rating_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rating", t => t.Rating_Id)
                .Index(t => t.Rating_Id);
            
            CreateTable(
                "dbo.Rating",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(unicode: false),
                        Description = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movie", "Rating_Id", "dbo.Rating");
            DropIndex("dbo.Movie", new[] { "Rating_Id" });
            DropTable("dbo.Rating");
            DropTable("dbo.Movie");
        }
    }
}
