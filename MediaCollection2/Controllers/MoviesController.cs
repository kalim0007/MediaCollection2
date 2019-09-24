using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediaCollection2.Data;
using MediaCollection2.Models;
using MediaCollection2.Domain;
using MediaCollection2.Models.Movies;
using MediaCollection2.Models.Review;
using MediaCollection2.Models.Genre;
using Microsoft.EntityFrameworkCore;

namespace MediaCollection2.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MediaCollectionContext context;
        public decimal teller { get; set; }
        public decimal totalrating { get; set; }
        public decimal avgrating { get; set; }
        public MoviesController(MediaCollectionContext context)
        {
            this.context = context;
        }
        // GET: Movies
        public ActionResult Index()
        {
            var model = new List<ListMovieViewModel>();
            var movies = context.Movies.Include(m => m.Director).Include(m => m.Writer);
            foreach (var movie in movies)
            {
                model.Add(new ListMovieViewModel()
                {
                    ID = movie.ID,
                    Titel =movie.Titel,
                    ReleaseDate = movie.ReleaseDate,
                    Lenght = movie.Lenght,
                    DirectorID = movie.DirectorID,
                    WriterID = movie.WriterID,
                    Director = movie.Director.Name,
                    Writer = movie.Writer.Name,
                });
            }

            return View(model);
        }

        // GET: Movies/Details/5
        public ActionResult Details(int id)
        {
            var movie = context.Movies.Include(m=>m.Reviews).Include(m=>m.Genres).FirstOrDefault(m=>m.ID==id);
            List<MovieReviewDetailsViewModel> Reviews = new List<MovieReviewDetailsViewModel>();
            List<MovieGenreCreateViewModel> genres = new List<MovieGenreCreateViewModel>();
            foreach (var review in movie.Reviews)
            {
                Reviews.Add(new MovieReviewDetailsViewModel()
                {
                    Comment = review.Comment,
                    ID = review.ID,
                    MovieID = review.MovieID,
                    Rating = review.Rating,
                });
            }
            foreach (var genre in movie.Genres)
            {
                genres.Add(new MovieGenreCreateViewModel() { Naam = genre.Naam });
            }
            var model = new DetailsMovieViewModel()
            {
                ID = movie.ID,
                Titel =movie.Titel,
                ReleaseDate = movie.ReleaseDate,
                Lenght = movie.Lenght,
                Reviews = Reviews,
                Genres = genres,
            };

            foreach (var review in movie.Reviews)
            {
                totalrating = totalrating + review.Rating;
                teller++;
            }
            if (teller != 0)
            {
                ViewBag.avg = avgrating = totalrating / teller;

            }
            if (movie == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateMovieViewModel model)
        {
            if (ModelState.IsValid)
            {
                context.Movies.Add(new Movie()
                {
                    Titel = model.Titel,
                    ReleaseDate = model.ReleaseDate,
                    Lenght = model.Lenght
                });
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movie = context.Movies.Find(id);
            if (movie==null)
            {
                return NotFound();
            }
            var model = new EditMovieViewModel()
            {
                ID = movie.ID,
                Titel =movie.Titel,
                ReleaseDate = movie.ReleaseDate,
                Lenght = movie.Lenght,
            };
            return View(model);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DetailsMovieViewModel model)
        {
            if (ModelState.IsValid)
            {
                var movie = context.Movies.Find(id);
                movie.Titel = model.Titel;
                movie.ReleaseDate = model.ReleaseDate;
                movie.Lenght = model.Lenght;
                context.Movies.Update(movie);
                context.SaveChanges();
            }
                return RedirectToAction(nameof(Index));
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movie = context.Movies.Find(id);
            var model = new DeleteMovieViewModel()
            {
                ID = movie.ID,
                Titel = movie.Titel
            };
            return View(model);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var movie = context.Movies.Find(id);
            context.Movies.Remove(movie);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}