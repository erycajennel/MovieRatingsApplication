namespace MovieRatings.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movie",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, unicode: false),
                        ReleaseDate = c.DateTime(precision: 0),
                        RatingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rating", t => t.RatingId, cascadeDelete: true)
                .Index(t => t.RatingId);
            
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
            DropForeignKey("dbo.Movie", "RatingId", "dbo.Rating");
            DropIndex("dbo.Movie", new[] { "RatingId" });
            DropTable("dbo.Rating");
            DropTable("dbo.Movie");
        }
    }
}
