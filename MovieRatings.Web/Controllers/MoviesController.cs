﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MovieRatings.Web.Models;

namespace MovieRatings.Web.Controllers
{
    public class MoviesController : ApiController
    {
        private MovieRatingsWebContext db = new MovieRatingsWebContext();

        // GET: api/Movies
        public IQueryable<MovieDTO> GetMovies()
        {
            var movies = from m in db.Movies
                         select new MovieDTO()
                         {
                             Id = m.Id
                             , Title = m.Title
                             , ReleaseDate = m.ReleaseDate
                             , Rating = m.Rating
                         };

            return movies;
        }

        // GET: api/Movies/5
        [ResponseType(typeof(MovieDTO))]
        public async Task<IHttpActionResult> GetMovie(int id)
        {
            var movie = await db.Movies.Include(m => m.Rating).Select(m =>
                            new MovieDTO()
                            {
                                Id = m.Id
                                , Title = m.Title
                                , ReleaseDate = m.ReleaseDate
                                , Rating = m.Rating
                            }
                        ).SingleOrDefaultAsync(b => b.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        // PUT: api/Movies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            db.Entry(movie).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Movies
        [ResponseType(typeof(MovieDTO))]
        public async Task<IHttpActionResult> PostMovie(Movie movie)
        {
            db.Movies.Add(movie);
            await db.SaveChangesAsync();
            
            db.Entry(movie).Reference(x => x.Rating).Load();

            var dto = new MovieDTO()
            {
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                RatingId = movie.RatingId
            };

            return CreatedAtRoute("DefaultApi", new { id = movie.Id }, dto);
        }

        // DELETE: api/Movies/5
        [ResponseType(typeof(Movie))]
        public async Task<IHttpActionResult> DeleteMovie(int id)
        {
            Movie movie = await db.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            db.Movies.Remove(movie);
            await db.SaveChangesAsync();

            return Ok(movie);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MovieExists(int id)
        {
            return db.Movies.Count(e => e.Id == id) > 0;
        }
    }
}