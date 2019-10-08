using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieRatings.Web.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}