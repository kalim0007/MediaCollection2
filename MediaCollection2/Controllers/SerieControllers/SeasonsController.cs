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
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace MediaCollection2.Controllers.SerieControllers
{
    [Authorize]
    public class SeasonsController : Controller
    {
        private readonly MediaCollectionContext _context;
        private readonly IHostingEnvironment hostingEnvironment;

        public SeasonsController(MediaCollectionContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: Series
        public async Task<IActionResult> Index()
        {
            var seasons = await _context.Seasons.Include(s=>s.Serie).ToListAsync();
            List<SeasonViewModel> model = new List<SeasonViewModel>();
            foreach (var season in seasons)
            {
                model.Add(new SeasonViewModel() { ID = season.ID, Titel = season.Titel , Serie = season.Serie.Titel, Nr = season.Nr, PhotoPath = season.PhotoPath, Rating = season.Rating });
            }
            return View(model);

        }

        // GET: Series/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seasons = await _context.Seasons.Include(s => s.Episodes)
                .FirstOrDefaultAsync(m => m.ID == id);
            List<EpisodesViewModel> episodes = new List<EpisodesViewModel>();
            foreach (var episode in seasons.Episodes)
            {
                episodes.Add(new EpisodesViewModel() { ID = episode.Season.ID, Titel = episode.Season.Titel, Nr = episode.Season.Nr });
            }
            if (seasons == null)
            {
                return NotFound();
            }
            var model = new SeasonDetailViewModel() { ID = seasons.ID, Titel = seasons.Titel, Episodes = episodes, Rating = seasons.Rating };
            return View(model);
        }

        // GET: Series/Create
        public IActionResult Create()
        {
            ViewData["SerieID"] = new SelectList(_context.Series, "ID", "Titel");
            return View();

        }

        // POST: Series/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SeasonViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    string UploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = GetUniqueFilmName(model.Photo.FileName);
                    string filePath = Path.Combine(UploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                _context.Add(new Season() { YoutubeTrailer = model.YoutubeTrailer, Titel = model.Titel, Nr = model.Nr, PhotoPath = uniqueFileName, SerieID = model.SerieID, Rating = model.Rating });
                await _context.SaveChangesAsync();
            }


            return RedirectToAction(nameof(Index));
        }

        // GET: Series/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var season =  _context.Seasons.Include(s=>s.Serie).FirstOrDefault(s=>s.ID==id);
            if (season == null)
            {
                return NotFound();
            }
            var model = new SeasonViewModel() { ID = season.ID, YoutubeTrailer = season.YoutubeTrailer, Titel = season.Titel, SerieID = season.Serie.ID, PhotoPath = season.PhotoPath, Rating = season.Rating, Nr = season.Nr };
            ViewData["SerieID"] = new SelectList(_context.Series, "ID", "Titel");
            return View(model);
        }

        // POST: Series/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int lastSerieID, SeasonViewModel model)
        {
                string uniqueFileName = null;
            if (ModelState.IsValid)
            {
                if (model.Photo != null)
                {
                    string UploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueFileName = GetUniqueFilmName(model.Photo.FileName);
                    string filePath = Path.Combine(UploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                var season = _context.Seasons.Find(model.ID);
                season.Titel = model.Titel;
                season.Nr = model.Nr;
                season.SerieID = model.SerieID;
                season.Rating = model.Rating;
                season.YoutubeTrailer = model.YoutubeTrailer;
                if (model.Photo!=null)
                {
                    season.PhotoPath = uniqueFileName;
                }
                _context.Update(season);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seasoon = await _context.Seasons
                .FirstOrDefaultAsync(m => m.ID == id);
            if (seasoon == null)
            {
                return NotFound();
            }
            var model = new SeasonViewModel() { ID = seasoon.ID, Titel = seasoon.Titel };
            return View(model);
        }

        // POST: Series/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var season = await _context.Seasons.FindAsync(id);
            _context.Seasons.Remove(season);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private string GetUniqueFilmName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                + "_"
                + Guid.NewGuid().ToString().Substring(0, 4)
                + Path.GetExtension(fileName);

        }
    }
}
