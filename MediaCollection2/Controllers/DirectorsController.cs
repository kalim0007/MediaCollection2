using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediaCollection2.Data;
using MediaCollection2.Domain;
using MediaCollection2.Models.Directors;

namespace MediaCollection2.Controllers
{
    public class DirectorsController : Controller
    {
        private readonly MediaCollectionContext _context;

        public DirectorsController(MediaCollectionContext context)
        {
            _context = context;
        }

        // GET: Directors
        public IActionResult Index()
        {
            var directors = _context.Directors.Include(d => d.Movies);
            var model = new List<MovieDirectorListViewModel>();

            foreach (var director in directors)
            {
                model.Add(new MovieDirectorListViewModel() { ID = director.ID, Name = director.Name, DateOfBirth = director.DateOfBirth, MovieID = director.MovieID, Movies = director.Movies.Titel });
            }
            return View(model);
        }

        // GET: Directors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = await _context.Directors
                .Include(d => d.Movies)
                .FirstOrDefaultAsync(m => m.ID == id);
            var model = new MovieDirectorDetailsViewModel() { ID = director.ID, Name = director.Name, DateOfBirth = director.DateOfBirth, MovieID = director.MovieID, Movies = director.Movies.Titel };
            if (director == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Directors/Create
        public IActionResult Create()
        {
            ViewData["MovieID"] = new SelectList(_context.Movies, "ID", "Titel");
            return View();
        }

        // POST: Directors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieDirectorCreateViewModel model )
        {
            if (ModelState.IsValid)
            {
                var director = new Director()
                {
                    MovieID = model.MovieID,
                    DateOfBirth = model.DateOfBirth,
                    Name = model.Name,
                };
                _context.Add(director);
                await _context.SaveChangesAsync();
            }
            ViewData["MovieID"] = new SelectList(_context.Movies, "ID", "Titel", model.MovieID);
            return RedirectToAction(nameof(Index));

        }

        // GET: Directors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = await _context.Directors.FindAsync(id);
            if (director == null)
            {
                return NotFound();
            }
            var model = new MovieDirectorEditViewModel() { ID = director.ID, Name = director.Name, DateOfBirth = director.DateOfBirth, MovieID = director.MovieID, Movies = director.Movies.Titel};
            ViewData["MovieID"] = new SelectList(_context.Movies, "ID", "Titel", director.MovieID);
            return View(model);
        }

        // POST: Directors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieDirectorEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var director = _context.Directors.Find(id);
                director.MovieID = model.MovieID;
                director.Name = model.Name;
                director.DateOfBirth = model.DateOfBirth;
                _context.Update(director);
                    await _context.SaveChangesAsync();

            }
            ViewData["MovieID"] = new SelectList(_context.Movies, "ID", "ID", model.MovieID);
            return RedirectToAction(nameof(Index));
        }

        // GET: Directors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = await _context.Directors
                .Include(d => d.Movies)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (director == null)
            {
                return NotFound();
            }
            var model = new MovieDirectorDeleteViewModel() { ID = director.ID, Name = director.Name, DateOfBirth = director.DateOfBirth, MovieID = director.MovieID, Movies = director.Movies.Titel };
            return View(model);
        }

        // POST: Directors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var director = await _context.Directors.FindAsync(id);
            _context.Directors.Remove(director);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
