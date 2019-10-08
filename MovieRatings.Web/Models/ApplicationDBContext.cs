using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MovieRatings.Web.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Movie> Movies
        {
            get;
            set;
        }

        public DbSet <Rating> Ratings
        {
            get;
            set;
        }

        // Keep Singular table names
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public ApplicationDBContext() : base("DefaultConnection")
        { }
    }

}