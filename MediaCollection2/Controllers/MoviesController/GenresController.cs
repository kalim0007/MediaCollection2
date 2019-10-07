using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediaCollection2.Data;
using MediaCollection2.Domain;
using MediaCollection2.Models.Genre;
using Microsoft.AspNetCore.Authorization;

namespace MediaCollection2.Controllers
{
    [Authorize]
    public class GenresController : Controller
    {
        private readonly MediaCollectionContext context;

        public GenresController(MediaCollectionContext Context)
        {
            context = Context;
        }

        // GET: Genres
        public IActionResult Index()
        {
            var model = new List<MovieGenreListViewModel>();
            var genres = context.Genres.Include(r => r.Movie);
            foreach (var genre in genres)
            {
                model.Add(new MovieGenreListViewModel()
                {
                    ID = genre.ID,
                    Naam = genre.Naam,
                    MovieID = genre.MovieID,
                    Movie = genre.Movie.Titel
                });
            }

            return View(model);
        }

        // GET: Genres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var genre = await context.Genres
                .Include(g => g.Movie)
                .FirstOrDefaultAsync(m => m.ID == id);
            var model = new GenreMovieDetailViewModel()
            {
                ID = genre.ID,
                Naam = genre.Naam,
                Movie = genre.Movie.Titel,
                MovieID = genre.MovieID,
            };
            if (genre == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // GET: Genres/Create
        public IActionResult Create()
        {
            ViewData["MovieID"] = new SelectList(context.Movies, "ID", "Titel");
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieGenreCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                context.Add(new Genre() {Naam = model.Naam, MovieID=model.MovieID});
                await context.SaveChangesAsync();
            }
            ViewData["MovieID"] = new SelectList(context.Movies, "ID", "Titel", model.MovieID);
            return RedirectToAction(nameof(Index));
        }

        // GET: Genres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            var model = new MovieGenreEditViewModel() { ID = genre.ID, MovieID = genre.MovieID, Naam=genre.Naam };
            ViewData["MovieID"] = new SelectList(context.Movies, "ID", "Titel", genre.MovieID);
            return View(model);
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieGenreEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var genre = context.Genres.Find(id);
                genre.MovieID = model.MovieID;
                genre.Naam = model.Naam;
                context.Update(genre);
                    await context.SaveChangesAsync();


                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieID"] = new SelectList(context.Movies, "ID", "Titel", model.MovieID);
            return RedirectToAction(nameof(Index));
        }

        // GET: Genres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await context.Genres
                .Include(g => g.Movie)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (genre == null)
            {
                return NotFound();
            }
            var model = new MovieGenreDeleteViewModel() { ID = genre.ID, MovieID = genre.MovieID, Naam = genre.Naam, Movie = genre.Movie.Titel };
            return View(model);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genre = await context.Genres.FindAsync(id);
            context.Genres.Remove(genre);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
