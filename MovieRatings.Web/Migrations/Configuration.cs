namespace MovieRatings.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MovieRatings.Web.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MovieRatings.Web.Models.MovieRatingsWebContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
        }

        protected override void Seed(MovieRatings.Web.Models.MovieRatingsWebContext context)
        {
            context.Ratings.AddOrUpdate(x => x.Id
                , new Rating() { Id = 1, Code = "PG", Description = "Parental Guidance" }
                , new Rating() { Id = 2, Code = "G", Description = "General Audiences" }
                , new Rating() { Id = 3, Code = "M", Description = "Mature" }
                , new Rating() { Id = 4, Code = "MA", Description = "Mature Accompanied" }
                , new Rating() { Id = 5, Code = "R", Description = "Restricted" }
            );


            context.Movies.AddOrUpdate(x => x.Id
                , new Movie() { Id = 1, Title = "Glass", ReleaseDate = new DateTime(2019, 1, 18), RatingId = 1 }
                , new Movie() { Id = 2, Title = "The Kid Who Would Be King", ReleaseDate = new DateTime(2019, 1, 25), RatingId = 1 }
                , new Movie() { Id = 3, Title = "The Lego Movie 2: The Second Part", ReleaseDate = new DateTime(2019, 2, 8), RatingId = 1 }
                , new Movie() { Id = 4, Title = "What Men Want", ReleaseDate = new DateTime(2019, 2, 8), RatingId = 5 }

            );
        }
    }
}
