using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediaCollection2.Data;
using MediaCollection2.Domain;
using MediaCollection2.Models.Wrtiters;
using Microsoft.AspNetCore.Authorization;

namespace MediaCollection2.Controllers
{
    [Authorize]
    public class WritersController : Controller
    {
        private readonly MediaCollectionContext _context;

        public WritersController(MediaCollectionContext context)
        {
            _context = context;
        }

        // GET: Writers
        public IActionResult Index()
        {
            var writers = _context.Writers.Include(w => w.Movies);
            List<MovieWriterListViewModel> model = new List<MovieWriterListViewModel>();
            foreach (var writer in writers)
            {
                model.Add(new MovieWriterListViewModel() { ID = writer.ID, Name = writer.Name, DateOfBirth = writer.DateOfBirth, MovieID = writer.MovieID, Movies = writer.Movies.Titel });
            }
            return View(model);
        }

        // GET: Writers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var writer = await _context.Writers
                .Include(w => w.Movies)
                .FirstOrDefaultAsync(m => m.ID == id);
            var model = new MovieWriterDetailsViewModel() { ID = writer.ID, Name = writer.Name, DateOfBirth = writer.DateOfBirth, MovieID = writer.MovieID, Movies = writer.Movies.Titel };
            if (writer == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Writers/Create
        public IActionResult Create()
        {
            ViewData["MovieID"] = new SelectList(_context.Movies, "ID", "Titel");
            return View();
        }

        // POST: Writers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieWriterCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var writer = new Writer() { Name = model.Name, DateOfBirth = model.DateOfBirth, MovieID = model.MovieID };
                _context.Add(writer);
                await _context.SaveChangesAsync();
            }
            ViewData["MovieID"] = new SelectList(_context.Movies, "ID", "Titel", model.MovieID);
            return RedirectToAction(nameof(Index));
        }

        // GET: Writers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var writer = await _context.Writers
                .Include(w => w.Movies)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (writer == null)
            {
                return NotFound();
            }
            var model = new MovieWriterEditViewModel() { ID = writer.ID, Name = writer.Name, DateOfBirth = writer.DateOfBirth, MovieID = writer.MovieID, Movies = writer.Movies.Titel };
            ViewData["MovieID"] = new SelectList(_context.Movies, "ID", "Titel", writer.MovieID);
            return View(model);
        }

        // POST: Writers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieWriterEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Writer writer = _context.Writers.Find(id);
                writer.MovieID = model.MovieID;
                writer.Name = model.Name;
                writer.DateOfBirth = model.DateOfBirth;
                _context.Update(writer);
                await _context.SaveChangesAsync();
            }
            ViewData["MovieID"] = new SelectList(_context.Movies, "ID", "Titel", model.MovieID);
            return RedirectToAction(nameof(Index));
        }

        // GET: Writers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var writer = await _context.Writers
                .Include(w => w.Movies)
                .FirstOrDefaultAsync(m => m.ID == id);
            var model = new MovieWriterDeleteViewModel() { ID = writer.ID, Name = writer.Name, DateOfBirth = writer.DateOfBirth, MovieID = writer.MovieID, Movies = writer.Movies.Titel };
            if (writer == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Writers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var writer = await _context.Writers.FindAsync(id);
            _context.Writers.Remove(writer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
