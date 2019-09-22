using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediaCollection2.Data;
using MediaCollection2.Domain;

namespace MediaCollection2.Controllers
{
    public class Movies1Controller : Controller
    {
        private readonly MediaCollectionContext _context;

        public Movies1Controller(MediaCollectionContext context)
        {
            _context = context;
        }

        // GET: Movies1
        public async Task<IActionResult> Index()
        {
            var mediaCollectionContext = _context.Movies.Include(m => m.Director).Include(m => m.Writer);
            return View(await mediaCollectionContext.ToListAsync());
        }

        // GET: Movies1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Director)
                .Include(m => m.Writer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies1/Create
        public IActionResult Create()
        {
            ViewData["DirectorID"] = new SelectList(_context.Directors, "ID", "ID");
            ViewData["WriterID"] = new SelectList(_context.Writers, "ID", "ID");
            return View();
        }

        // POST: Movies1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Titel,ReleaseDate,GenreID,DirectorID,WriterID,UserID")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DirectorID"] = new SelectList(_context.Directors, "ID", "ID", movie.DirectorID);
            ViewData["WriterID"] = new SelectList(_context.Writers, "ID", "ID", movie.WriterID);
            return View(movie);
        }

        // GET: Movies1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["DirectorID"] = new SelectList(_context.Directors, "ID", "ID", movie.DirectorID);
            ViewData["WriterID"] = new SelectList(_context.Writers, "ID", "ID", movie.WriterID);
            return View(movie);
        }

        // POST: Movies1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Titel,ReleaseDate,GenreID,DirectorID,WriterID,UserID")] Movie movie)
        {
            if (id != movie.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DirectorID"] = new SelectList(_context.Directors, "ID", "ID", movie.DirectorID);
            ViewData["WriterID"] = new SelectList(_context.Writers, "ID", "ID", movie.WriterID);
            return View(movie);
        }

        // GET: Movies1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Director)
                .Include(m => m.Writer)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.ID == id);
        }
    }
}
