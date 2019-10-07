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
using MediaCollection2.Models.MusicModels.MusicDirector;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace MediaCollection2.Controllers.MusicControllers
{
    [Authorize]
    public class MusicDirectorsController : Controller
    {
        private readonly MediaCollectionContext _context;
        private readonly IHostingEnvironment hostingEnvironment;

        public MusicDirectorsController(MediaCollectionContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: MusicDirectors
        public IActionResult Index()
        {
            var Directors = _context.MusicDirectors.Include(m => m.Music);
            List<MusicDirectorViewModel> model = new List<MusicDirectorViewModel>();
            foreach (var directors in Directors)
            {
                model.Add(new MusicDirectorViewModel()
                {
                    Music = directors.Music.Titel,
                    Name = directors.Name,
                    DateOfBirth = directors.DateOfBirth,
                    MusicID = directors.MusicID,
                    PhotoPath = directors.PhotoPath,
                    ID = directors.ID
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

            var director = await _context.MusicDirectors
                .Include(m => m.Music)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (director == null)
            {
                return NotFound();
            }
            var model = (new MusicDirectorViewModel()
            {
                Music = director.Music.Titel,
                Name = director.Name,
                MusicID = director.MusicID,
                DateOfBirth = director.DateOfBirth,
                PhotoPath = director.PhotoPath,
                ID = director.ID
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
        public async Task<IActionResult> Create(MusicDirectorViewModel model)
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
                _context.MusicDirectors.Add(new MusicDirector() { Name = model.Name, MusicID = model.MusicID, PhotoPath= uniqueFileName, DateOfBirth=model.DateOfBirth });
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

            var director = _context.MusicDirectors.Include(m => m.Music).FirstOrDefault(m => m.ID == id);
            var model = new MusicDirectorViewModel()
            {
                Music = director.Music.Titel,
                Name = director.Name,
                MusicID = director.MusicID,
                PhotoPath = director.PhotoPath,
                DateOfBirth = director.DateOfBirth,
                ID = director.ID
            };
            if (director == null)
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
        public async Task<IActionResult> Edit(MusicDirectorViewModel model)
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
                var director = await _context.MusicDirectors.FindAsync(model.ID);
                director.Name = model.Name;
                director.MusicID = model.MusicID;
                director.DateOfBirth = model.DateOfBirth;
                if (model.Photo!=null)
                {
                    director.PhotoPath = uniqueFileName;
                }
                _context.Update(director);
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

            var director = await _context.MusicDirectors
                .Include(m => m.Music)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (director == null)
            {
                return NotFound();
            }
            var model = new MusicDirectorViewModel()
            {
                Music = director.Music.Titel,
                Name = director.Name,
                DateOfBirth = director.DateOfBirth,
                MusicID = director.MusicID,
                ID = director.ID
            };
            return View(model);
        }

        // POST: MusicGenres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var director = await _context.MusicDirectors.FindAsync(id);
            _context.MusicDirectors.Remove(director);
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
