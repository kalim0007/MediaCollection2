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
using Microsoft.EntityFrameworkCore;

namespace MediaCollection2.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MediaCollectionContext context;

        public MoviesController(MediaCollectionContext context)
        {
            this.context = context;
        }
        // GET: Movies
        public ActionResult Index()
        {
            var model = new List<ListMovieViewModel>();
            foreach (var movie in context.Movies.Include(d=>d.Director).Include(w=>w.Writer).Include(g=>g.Genres))
            {
                model.Add(new ListMovieViewModel()
                {
                    ID = movie.ID,
                    Titel = movie.Titel,
                    DirectorName = movie.Director.Name,
                    WriterName = movie.Writer.Name,
                    ReleaseDate = movie.ReleaseDate,
                });
            }
            return View(model);
        }

        // GET: Movies/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
                    ReleaseDate = model.ReleaseDate,
                    Titel = model.Titel,
                    Director =null,
                    Writer = null,
                    Genres =null,
                    Rviews = null,
                });
                context.SaveChanges();
            }
            return RedirectToAction("Index");

        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Movies/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}