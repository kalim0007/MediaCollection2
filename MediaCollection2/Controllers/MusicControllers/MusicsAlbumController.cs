using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediaCollection2.Data;
using MediaCollection2.Domain.music;
using MediaCollection2.Models.MusicModels;
using MediaCollection2.Models.MusicModels.MusicAlbum;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace MediaCollection2.Controllers.MusicControllers
{
    [Authorize]
    public class MusicsAlbumController : Controller
    {
        private readonly MediaCollectionContext _context;
        private readonly IHostingEnvironment hostingEnvironment;

        public MusicsAlbumController(MediaCollectionContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: MoviePlaylists
        public IActionResult Index()
        {
            List<MusicAlbumViewModel> model = new List<MusicAlbumViewModel>();
            foreach (var album in _context.Albums)
            {
                model.Add(new MusicAlbumViewModel()
                {
                    ID = album.ID,
                    Naam = album.Naam,
                    PhotoPath = album.PhotoPath
                });
            }
            return View(model);
        }

        // GET: MoviePlaylists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .FirstOrDefaultAsync(m => m.ID == id);
            List<MusicViewModels> musics = new List<MusicViewModels>();
            foreach (var music in _context.MusicAlbums.Include(m => m.Musics).Where(m => m.AlbumID == id))
            {
                musics.Add(new MusicViewModels() { Titel = music.Musics.Titel, ReleaseDate = music.Musics.ReleaseDate, Lenght = music.Musics.Lenght });
            }
            if (album == null)
            {
                return NotFound();
            }
            var model = new MusicAlbumViewModel()
            {
                ID = album.ID,
                Naam = album.Naam,
                Musics = musics,
            };
            return View(model);
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
        public async Task<IActionResult> Create(MusicAlbumViewModel model)
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
                _context.Add(new Album() { ID = model.ID, Naam = model.Naam, PhotoPath = uniqueFileName });
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: MoviePlaylists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            var model = new MusicAlbumViewModel()
            {
                ID = album.ID,
                Naam = album.Naam,
                PhotoPath = album.PhotoPath,
            };
            return View(model);
        }

        // POST: MoviePlaylists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MusicAlbumViewModel model)
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
                var album = _context.Albums.Find(model.ID);
                album.Naam = model.Naam;
                if (model.Photo!=null)
                {
                    album.PhotoPath = uniqueFileName;
                }
                _context.Update(album);
                await _context.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));

        }

        // GET: MoviePlaylists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .FirstOrDefaultAsync(m => m.ID == id);

            if (album == null)
            {
                return NotFound();
            }
            var model = new MusicAlbumViewModel()
            {
                ID = album.ID,
                Naam = album.Naam,
            };
            return View(model);
        }

        // POST: MoviePlaylists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await _context.Albums.FindAsync(id);
            _context.Albums.Remove(album);
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
