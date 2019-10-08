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
    public class SeriesController : Controller
    {
        private readonly MediaCollectionContext _context;

        public SeriesController(MediaCollectionContext context)
        {
            _context = context;
        }

        // GET: Series
        public async Task<IActionResult> Index()
        {
            var series = await _context.Series.ToListAsync();
            List<SerieViewModel> model = new List<SerieViewModel>();
            foreach (var serie in series)
            {
                model.Add(new SerieViewModel() { ID = serie.ID, Titel = serie.Titel });
            }
            return View(model);

        }

        // GET: Series/Details/5
    [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serie = await _context.Series.Include(s => s.Season)
                .FirstOrDefaultAsync(m => m.ID == id);
            List<SeasonViewModel> seasons = new List<SeasonViewModel>();
            foreach (var season in serie.Season)
            {
                seasons.Add(new SeasonViewModel() { ID = season.ID, PhotoPath = season.PhotoPath, Nr = season.Nr });
            }
            if (serie == null)
            {
                return NotFound();
            }
            var model = new SerieDetailViewModel() { ID = serie.ID, Titel = serie.Titel, Season = seasons };
            return View(model);
        }

        // GET: Series/Create
    [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Series/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    [Authorize]
        public async Task<IActionResult> Create(SerieViewModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(new Serie() { Titel = model.Titel });
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Series/Edit/5
    [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serie = await _context.Series.FindAsync(id);
            if (serie == null)
            {
                return NotFound();
            }
            var model = new SerieViewModel() { ID = serie.ID, Titel = serie.Titel };
            return View(model);
        }

        // POST: Series/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    [Authorize]
        public async Task<IActionResult> Edit(SerieViewModel model)
        {
            if (ModelState.IsValid)
            {
                var serie = _context.Series.Find(model.ID);
                serie.Titel = model.Titel;
                _context.Update(serie);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serie = await _context.Series
                .FirstOrDefaultAsync(m => m.ID == id);
            if (serie == null)
            {
                return NotFound();
            }
            var model = new SerieViewModel() { ID = serie.ID, Titel = serie.Titel };
            return View(model);
        }

        // POST: Series/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serie = await _context.Series.FindAsync(id);
            _context.Series.Remove(serie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
