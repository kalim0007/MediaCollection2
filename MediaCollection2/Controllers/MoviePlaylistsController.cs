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
    public class MoviePlaylistsController : Controller
    {
        private readonly MediaCollectionContext _context;

        public MoviePlaylistsController(MediaCollectionContext context)
        {
            _context = context;
        }

        // GET: MoviePlaylists
        public async Task<IActionResult> Index()
        {
            return View(await _context.MoviePlaylists.ToListAsync());
        }

        // GET: MoviePlaylists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moviePlaylist = await _context.MoviePlaylists
                .FirstOrDefaultAsync(m => m.ID == id);
            if (moviePlaylist == null)
            {
                return NotFound();
            }

            return View(moviePlaylist);
        }

        // GET: MoviePlaylists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MoviePlaylists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Naam")] MoviePlaylist moviePlaylist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moviePlaylist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(moviePlaylist);
        }

        // GET: MoviePlaylists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moviePlaylist = await _context.MoviePlaylists.FindAsync(id);
            if (moviePlaylist == null)
            {
                return NotFound();
            }
            return View(moviePlaylist);
        }

        // POST: MoviePlaylists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Naam")] MoviePlaylist moviePlaylist)
        {
            if (id != moviePlaylist.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moviePlaylist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoviePlaylistExists(moviePlaylist.ID))
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
            return View(moviePlaylist);
        }

        // GET: MoviePlaylists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moviePlaylist = await _context.MoviePlaylists
                .FirstOrDefaultAsync(m => m.ID == id);
            if (moviePlaylist == null)
            {
                return NotFound();
            }

            return View(moviePlaylist);
        }

        // POST: MoviePlaylists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moviePlaylist = await _context.MoviePlaylists.FindAsync(id);
            _context.MoviePlaylists.Remove(moviePlaylist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoviePlaylistExists(int id)
        {
            return _context.MoviePlaylists.Any(e => e.ID == id);
        }
    }
}
