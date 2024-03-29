﻿using System;
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
using MediaCollection2.Models.MoviesModels.Movies;

namespace MediaCollection2.Areas.Movies
{
    public class MoviesController : Controller
    {
        private readonly MediaCollectionContext context;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly UserManager<IdentityUser> userManager;

        public decimal teller { get; set; }
        public decimal totalrating { get; set; }
        public decimal avgrating { get; set; }
        public MoviesController(MediaCollectionContext context, IHostingEnvironment hostingEnvironment, UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
        }

        public ActionResult Index()
        {
            var userId = userManager.GetUserId(HttpContext.User);
            var model = new ListMovieViewModel();
            model.Movies = new List<MovieViewModel>();
            var movies = context.Movies;
            var UserPlaylists = context.MoviePlaylists.Include(p=>p.Movies).Where(p => p.UserId == userId).ToList();
            foreach (var movie in movies)
            {
                var selectlistitems = new List<SelectListItem>();

                foreach (var playlist in UserPlaylists)
                {
                    if (!context.MoviePlaylistCombs.Any(p=>p.MovieID==movie.ID&& p.MoviePlaylistID==playlist.ID))
                    {
                        selectlistitems.Add(new SelectListItem() { Value = playlist.ID.ToString(), Text = playlist.Naam });
                    }
                }
                model.Movies.Add(new MovieViewModel()
                {
                    ID = movie.ID,
                    Titel = movie.Titel,
                    ReleaseDate = movie.ReleaseDate,
                    Lenght = movie.Lenght,
                    PhotoPath = movie.PhotoPath,
                    WantToWatch = movie.WantToWatch,
                    Watched = movie.WantToWatch,
                    Playlist = selectlistitems,
                });
            }
            ViewData["Playlist"] = new SelectList(context.MoviePlaylists, "ID", "Naam");
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var userId = userManager.GetUserName(HttpContext.User);
            var movie = context.Movies.Include(m => m.Reviews).Include(m => m.Genres).Include(m => m.Directors).Include(m => m.Writers).FirstOrDefault(m => m.ID == id);
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
                directors.Add(new MovieDirectorCreateViewModel() { Name = director.Name, DateOfBirth = director.DateOfBirth, Movie = director.Movies.Titel , ID=director.ID });
            }
            foreach (var writer in movie.Writers)
            {
                writers.Add(new MovieWriterCreateViewModel() { Name = writer.Name, DateOfBirth = writer.DateOfBirth, Movie = writer.Movies.Titel, ID = writer.ID });
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
                WantToWatch = movie.WantToWatch,
                Watched = movie.Watched,
                Youtube = movie.YoutubeTrailer,
                User = userId,
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

        [HttpPost]
        public ActionResult MovieToPlaylist(int MovieID, ListMovieViewModel model)
        {
            var result = context.MoviePlaylistCombs.FirstOrDefault(p => p.MovieID == MovieID && p.MoviePlaylistID == model.Playlist);
            if (result==null&&model.Playlist!=0)
            {
                context.MoviePlaylistCombs.Add(new MoviePlaylistComb() { MovieID = MovieID, MoviePlaylistID = model.Playlist });
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(CreateMovieViewModel model)
        {
            var userId = userManager.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    string UploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
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
                    WantToWatch = model.WantToWatch,
                    Watched = model.Watched,
                    YoutubeTrailer = model.YoutubeTrailer
                });
                context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));

        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movie = context.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }
            var model = new EditMovieViewModel()
            {
                ID = movie.ID,
                Titel = movie.Titel,
                ReleaseDate = movie.ReleaseDate,
                Lenght = movie.Lenght,
                WantToWatch = movie.WantToWatch,
                Watched = movie.Watched,
                YoutubeTrailer = movie.YoutubeTrailer,
            };
            ViewBag.PlaylistID = new SelectList(context.MoviePlaylists, "ID", "Naam");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(int id, EditMovieViewModel model)
        {
            if (ModelState.IsValid)
            {
                var movie = context.Movies.Find(id);
                movie.Titel = model.Titel;
                movie.ReleaseDate = model.ReleaseDate;
                movie.Lenght = model.Lenght;
                movie.WantToWatch = model.WantToWatch;
                movie.Watched = model.Watched;
                movie.YoutubeTrailer = model.YoutubeTrailer;

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

            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
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