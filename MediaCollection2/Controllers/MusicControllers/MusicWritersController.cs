using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediaCollection2.Data;
using MediaCollection2.Domain.music;
using Microsoft.AspNetCore.Hosting;
using MediaCollection2.Models.MusicModels.MusicWriter;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace MediaCollection2.Controllers.MusicControllers
{
    [Authorize]
    public class MusicWritersController : Controller
    {
        private readonly MediaCollectionContext _context;
        private readonly IHostingEnvironment hostingEnvironment;

        public MusicWritersController(MediaCollectionContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: MusicDirectors
        public IActionResult Index()
        {
            var Writers = _context.MusicWriters.Include(m => m.Music);
            List<MusicWriterViewModel> model = new List<MusicWriterViewModel>();
            foreach (var writer in Writers)
            {
                model.Add(new MusicWriterViewModel()
                {
                    Music = writer.Music.Titel,
                    Name = writer.Name,
                    DateOfBirth = writer.DateOfBirth,
                    MusicID = writer.MusicID,
                    PhotoPath = writer.PhotoPath,
                    ID = writer.ID
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

            var writer = await _context.MusicWriters
                .Include(m => m.Music)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (writer == null)
            {
                return NotFound();
            }
            var model = (new MusicWriterViewModel()
            {
                Music = writer.Music.Titel,
                Name = writer.Name,
                MusicID = writer.MusicID,
                DateOfBirth = writer.DateOfBirth,
                PhotoPath = writer.PhotoPath,
                ID = writer.ID
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
        public async Task<IActionResult> Create(MusicWriterViewModel model)
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
                _context.MusicWriters.Add(new MusicWriter() { Name = model.Name, MusicID = model.MusicID, PhotoPath = uniqueFileName, DateOfBirth = model.DateOfBirth });
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

            var writer = _context.MusicWriters.Include(m => m.Music).FirstOrDefault(m => m.ID == id);
            var model = new MusicWriterViewModel()
            {
                Music = writer.Music.Titel,
                Name = writer.Name,
                MusicID = writer.MusicID,
                PhotoPath = writer.PhotoPath,
                DateOfBirth = writer.DateOfBirth,
                ID = writer.ID
            };
            if (writer == null)
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
        public async Task<IActionResult> Edit(MusicWriterViewModel model)
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
                var writer = await _context.MusicWriters.FindAsync(model.ID);
                writer.Name = model.Name;
                writer.MusicID = model.MusicID;
                writer.DateOfBirth = model.DateOfBirth;
                if (model.Photo != null)
                {
                    writer.PhotoPath = uniqueFileName;
                }
                _context.Update(writer);
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

            var writer = await _context.MusicWriters
                .Include(m => m.Music)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (writer == null)
            {
                return NotFound();
            }
            var model = new MusicWriterViewModel()
            {
                Music = writer.Music.Titel,
                Name = writer.Name,
                DateOfBirth = writer.DateOfBirth,
                MusicID = writer.MusicID,
                ID = writer.ID
            };
            return View(model);
        }

        // POST: MusicGenres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var writer = await _context.MusicWriters.FindAsync(id);
            _context.MusicWriters.Remove(writer);
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
