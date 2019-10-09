using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediaCollection2.Data;
using MediaCollection2.Domain.Series;
using MediaCollection2.Models.SeriesModels;
using Microsoft.AspNetCore.Authorization;

namespace MediaCollection2.Controllers.SerieControllers
{
    public class EpisodesController : Controller
    {
        private readonly MediaCollectionContext _context;

        public EpisodesController(MediaCollectionContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var Episodes = _context.Episodes.Include(e => e.Season);
            List<EpisodesViewModel> model = new List<EpisodesViewModel>();
            foreach (var episode in Episodes)
            {
                model.Add(new EpisodesViewModel() { ID = episode.ID, Nr = episode.Nr, Season = episode.Season.Titel, Titel = episode.Titel});
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episode = await _context.Episodes
                .Include(e => e.Season)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (episode == null)
            {
                return NotFound();
            }
            var model = new EpisodesViewModel() { ID = episode.ID, Nr = episode.Nr, Season = episode.Season.Titel, Titel = episode.Titel, Length = episode.Length };
            return View(model);
        }

    [Authorize]
        public IActionResult Create()
        {
            ViewData["SeasonID"] = new SelectList(_context.Seasons, "ID", "Titel");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
    [Authorize]
        public async Task<IActionResult> Create(EpisodesViewModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(new Episode() {Length = model.Length, SeasonID = model.SeasonID, Nr = model.Nr, Titel = model.Titel });
                await _context.SaveChangesAsync();
            }
            ViewData["SeasonID"] = new SelectList(_context.Seasons, "ID", "Titel");
            return RedirectToAction(nameof(Index));

        }

    [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episode =  _context.Episodes.Include(e=>e.Season).FirstOrDefault(s=>s.ID==id);
            if (episode == null)
            {
                return NotFound();
            }
            var model = new EpisodesViewModel() { Nr = episode.Nr, Season = episode.Season.Titel, Titel = episode.Titel, Length = episode.Length, SeasonID=episode.SeasonID };
            ViewData["SeasonID"] = new SelectList(_context.Seasons, "ID", "Titel", episode.SeasonID);
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
    [Authorize]
        public async Task<IActionResult> Edit(EpisodesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var episode = await _context.Episodes.FindAsync(model.ID);
                episode.Length = model.Length;
                episode.Titel = model.Titel;
                episode.SeasonID = model.SeasonID;
                episode.Nr = model.Nr;
                _context.Update(episode);
                    await _context.SaveChangesAsync();
            }
            ViewData["SeasonID"] = new SelectList(_context.Seasons, "ID", "ID");
            return RedirectToAction(nameof(Index));

        }

    [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var episode = await _context.Episodes
                .Include(e => e.Season)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (episode == null)
            {
                return NotFound();
            }
            var model = new EpisodesViewModel() { Nr = episode.Nr, Titel = episode.Titel};

            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
    [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var episode = await _context.Episodes.FindAsync(id);
            _context.Episodes.Remove(episode);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EpisodeExists(int id)
        {
            return _context.Episodes.Any(e => e.ID == id);
        }
    }
}
