using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediaCollection2.Data;
using MediaCollection2.Models.Movies;
using MediaCollection2.Domain.Movie;

namespace MediaCollection2.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MediaCollectionContext context;

        public MoviesController(MediaCollectionContext Context )
        {
            context = Context;
        }
        public IActionResult MoviesList()
        {
            List<ListMovieViewModel> model = new List<ListMovieViewModel>();
            var movies = context.Movies;
            foreach (var movie in movies)
            {
                model.Add(new ListMovieViewModel()
                {
                    ID = movie.ID,
                    Titel = movie.Titel,
                    ReleaseDate = movie.ReleaseDate,
                });
            }
            return View(model);
        }
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult Create(CreateMovieViewModel model)
        {
            context.Movies.Add(new Movie()
            {
                Titel = model.Titel,
                ReleaseDate = model.ReleaseDate,
            });
            context.SaveChanges();
            return RedirectToAction("MoviesList");
        }
    }
}