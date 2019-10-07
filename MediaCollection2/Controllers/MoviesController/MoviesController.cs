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
using MediaCollection2.Models.Directors;
using MediaCollection2.Models.Wrtiters;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace MediaCollection2.Areas.Movies
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly MediaCollectionContext context;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly UserManager<IdentityUser> userManager;

        public decimal teller { get; set; }
        public decimal totalrating { get; set; }
        public decimal avgrating { get; set; }
        public MoviesController(MediaCollectionContext context, IHostingEnvironment hostingEnvironment,UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
        }

        // GET: Movies
        public ActionResult Index()
        {
            var model = new List<ListMovieViewModel>();
            var movies = context.Movies;
            foreach (var movie in movies)
            {
                model.Add(new ListMovieViewModel()
                {
                    ID = movie.ID,
                    Titel =movie.Titel,
                    ReleaseDate = movie.ReleaseDate,
                    Lenght = movie.Lenght,
                    PhotoPath =movie.PhotoPath,
                    WantToListen = movie.WantToListen,
                    Listened = movie.Listened,
                    
                });
            }
            return View(model);
        }

        // GET: Movies/Details/5
        public ActionResult Details(int id)
        {
            var movie = context.Movies.Include(m=>m.Reviews).Include(m=>m.Genres).Include(m => m.Directors).Include(m => m.Writers).FirstOrDefault(m=>m.ID==id);
            List<MovieReviewDetailsViewModel> Reviews = new List<MovieReviewDetailsViewModel>();
            List<MovieGenreCreateViewModel> genres = new List<MovieGenreCreateViewModel>();
            List<MovieDirectorCreateViewModel> directors = new List<MovieDirectorCreateViewModel>();
            List<MovieWriterCreateViewModel> writers = new List<MovieWriterCreateViewModel>();
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
            foreach (var director in movie.Directors)
            {
                directors.Add(new MovieDirectorCreateViewModel() { Name = director.Name, DateOfBirth = director.DateOfBirth, Movie=director.Movies.Titel });
            }
            foreach (var writer in movie.Writers)
            {
                writers.Add(new MovieWriterCreateViewModel() { Name = writer.Name, DateOfBirth = writer.DateOfBirth, Movie = writer.Movies.Titel });
            }
            var model = new DetailsMovieViewModel()
            {
                ID = movie.ID,
                Titel = movie.Titel,
                ReleaseDate = movie.ReleaseDate,
                Lenght = movie.Lenght,
                Reviews = Reviews,
                Genres = genres,
                Directors = directors,
                Writers = writers,
                PhotoPath = movie.PhotoPath,
                WantToListen = movie.WantToListen,
                Listened = movie.Listened,
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
            ViewData["PlaylistID"] = new SelectList(context.MoviePlaylists, "ID", "Titel");
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateMovieViewModel model)
        {
            var userId = userManager.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo!=null)
                {
                    string UploadsFolder =  Path.Combine(hostingEnvironment.WebRootPath, "images");
                     uniqueFileName = GetUniqueFileName(model.Photo.FileName);
                    string filePath = Path.Combine(UploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                context.Movies.Add(new Movie()
                {
                    Titel = model.Titel,
                    ReleaseDate = model.ReleaseDate,
                    Lenght = model.Lenght,
                    PhotoPath = uniqueFileName,
                    WantToListen = model.WantToListen,
                    Listened = model.Listened,
                }) ; 
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
                WantToListen = movie.WantToListen,
                Listened = movie.Listened,
            };
            ViewBag.PlaylistID = new SelectList(context.MoviePlaylists, "ID", "Naam");

            return View(model);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditMovieViewModel model)
        {
            if (ModelState.IsValid)
            {
                var movie = context.Movies.Find(id);
                movie.Titel = model.Titel;
                movie.ReleaseDate = model.ReleaseDate;
                movie.Lenght = model.Lenght;
                movie.WantToListen = model.WantToListen;
                movie.Listened = model.Listened;

                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    string UploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = GetUniqueFileName(model.Photo.FileName);
                    string filePath = Path.Combine(UploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    movie.PhotoPath = uniqueFileName;
                }
                context.Movies.Update(movie);
                context.SaveChanges();
                context.MoviePlaylistCombs.Add(new MoviePlaylistComb() { MovieID = model.ID, MoviePlaylistID = model.PlaylistID });
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
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
    }
}