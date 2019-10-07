using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediaCollection2.Data;
using MediaCollection2.Domain.music;
using MediaCollection2.Models.MusicModels.MusicGenre;
using Microsoft.AspNetCore.Authorization;

namespace MediaCollection2.Controllers.MusicControllers
{
    [Authorize]
    public class MusicGenresController : Controller
    {
        private readonly MediaCollectionContext _context;

        public MusicGenresController(MediaCollectionContext context)
        {
            _context = context;
        }

        // GET: MusicGenres
        public IActionResult Index()
        {
            var Genres = _context.MusicGenres.Include(m => m.Musics);
            List<MusicGenreViewModel> model = new List<MusicGenreViewModel>();
            foreach (var genre in Genres)
            {
                model.Add(new MusicGenreViewModel()
                {
                    Music = genre.Musics.Titel,
                    Naam = genre.Naam,
                    MusicID = genre.MusicID,
                    ID = genre.ID
                });
            }
            return View(model);
        }

        // GET: MusicGenres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _context.MusicGenres
                .Include(m => m.Musics)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (genre == null)
            {
                return NotFound();
            }
            var model = (new MusicGenreViewModel()
            {
                Music = genre.Musics.Titel,
                Naam = genre.Naam,
                ID = genre.ID
            });
            return View(model);
        }

        // GET: MusicGenres/Create
        public IActionResult Create()
        {
            ViewData["MusicID"] = new SelectList(_context.Musics, "ID", "Titel");
            return View();
        }

        // POST: MusicGenres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MusicGenreViewModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(new MusicGenre() { Naam=model.Naam, MusicID = model.MusicID });
                await _context.SaveChangesAsync();
            }
            ViewData["MusicsID"] = new SelectList(_context.Musics, "ID", "Titel");
            return RedirectToAction(nameof(Index));
        }

        // GET: MusicGenres/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = _context.MusicGenres.Include(m => m.Musics).FirstOrDefault(m => m.ID == id);
            var model = new MusicGenreViewModel()
            {
                Music = genre.Musics.Titel,
                Naam = genre.Naam,
                MusicID = genre.MusicID,
                ID = genre.ID
            };
            if (genre == null)
            {
                return NotFound();
            }
            ViewData["MusicID"] = new SelectList(_context.Musics, "ID", "Titel");
            return View(model);
        }

        // POST: MusicGenres/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MusicGenreViewModel model)
        {
            if (ModelState.IsValid)
            {
                var genre = await _context.MusicGenres.FindAsync(model.ID);
                genre.Naam = model.Naam;
                genre.MusicID = model.MusicID;
                _context.Update(genre);
                await _context.SaveChangesAsync();

            }
            ViewData["MusicsID"] = new SelectList(_context.Musics, "ID", "Titel");
            return RedirectToAction(nameof(Index));
        }

        // GET: MusicGenres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genre = await _context.MusicGenres
                .Include(m => m.Musics)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (genre == null)
            {
                return NotFound();
            }
            var model = new MusicGenreViewModel()
            {
                Music = genre.Musics.Titel,
                Naam = genre.Naam,
                ID = genre.ID
            };
            return View(model);
        }

        // POST: MusicGenres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var musicGenre = await _context.MusicGenres.FindAsync(id);
            _context.MusicGenres.Remove(musicGenre);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusicGenreExists(int id)
        {
            return _context.MusicGenres.Any(e => e.ID == id);
        }
    }
}
