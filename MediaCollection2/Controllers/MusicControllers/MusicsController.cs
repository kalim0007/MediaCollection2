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
using System.IO;
using Microsoft.AspNetCore.Hosting;
using MediaCollection2.Models;
using MediaCollection2.Models.MusicModels.MusicGenre;
using MediaCollection2.Models.MusicModels.MusicDirector;
using MediaCollection2.Models.MusicModels.MusicWriter;
using MediaCollection2.Models.MusicModels.Music;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MediaCollection2.Models.MusicModels.MusicPlaylist;

namespace MediaCollection2.Controllers
{
    public class MusicsController : Controller
    {
        private readonly MediaCollectionContext _context;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly UserManager<IdentityUser> userManager;

        public decimal teller { get; set; }
        public decimal totalrating { get; set; }
        public decimal avgrating { get; set; }

        public MusicsController(MediaCollectionContext context, IHostingEnvironment hostingEnvironment, UserManager<IdentityUser> userManager)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
            this.userManager = userManager;
        }
        [HttpPost]
        public ActionResult MusicToPlaylist(int MusicID, MusicListViewModel model)
        {
            if (model.Playlist != 0)
            {
                _context.MusicPlaylistCombs.Add(new MusicPlaylistComb() { MusicID = MusicID, MusicPlaylistID = model.Playlist });
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Index()
        {
            var userId = userManager.GetUserId(HttpContext.User);
            var model = new MusicListViewModel();
            model.Musics = new List<MusicViewModels>();
            var musics = _context.Musics;
            var UserPlaylists = _context.MusicPlaylists.Include(p => p.Musics).Where(p => p.UserId == userId).ToList();
            foreach (var music in musics)
            {
                var selectlistitems = new List<SelectListItem>();

                foreach (var playlist in UserPlaylists)
                {
                    if (!_context.MusicPlaylistCombs.Any(p => p.MusicID == music.ID && p.MusicPlaylistID == playlist.ID))
                    {
                        selectlistitems.Add(new SelectListItem() { Value = playlist.ID.ToString(), Text = playlist.Naam });
                    }
                }
                model.Musics.Add(new MusicViewModels()
                {
                    ID = music.ID,
                    Titel = music.Titel,
                    ReleaseDate = music.ReleaseDate,
                    Lenght = music.Lenght,
                    PhotoPath = music.PhotoPath,
                    WantToListen = music.WantToListen,
                    Listened = music.Listened,
                    Playlist = selectlistitems,
                });
            }
            ViewData["playlist"] = new SelectList(_context.MusicPlaylists, "ID", "Naam");
            return View(model);
        }

        // GET: Musics/Details/5
    [Authorize]
        public IActionResult Details(int? id)
        {
            var music = _context.Musics.Include(m => m.Reviews).Include(m => m.Genres).Include(m => m.Directors).Include(m => m.Writers).FirstOrDefault(m => m.ID == id);
            List<MusicReviewViewModel> Reviews = new List<MusicReviewViewModel>();
            List<MusicGenreViewModel> genres = new List<MusicGenreViewModel>();
            List<MusicDirectorViewModel> directors = new List<MusicDirectorViewModel>();
            List<MusicWriterViewModel> writers = new List<MusicWriterViewModel>();
            foreach (var review in music.Reviews)
            {
                Reviews.Add(new MusicReviewViewModel()
                {
                    Comment = review.Comment,
                    ID = review.ID,
                    MusicID = review.MusicsID,
                    Rating = review.Rating,
                });
            }
            foreach (var genre in music.Genres)
            {
                genres.Add(new MusicGenreViewModel() { Naam = genre.Naam });
            }
            foreach (var director in music.Directors)
            {
                directors.Add(new MusicDirectorViewModel() { Name = director.Name, DateOfBirth = director.DateOfBirth, Music = director.Music.Titel });
            }
            foreach (var writer in music.Writers)
            {
                writers.Add(new MusicWriterViewModel() { Name = writer.Name, DateOfBirth = writer.DateOfBirth, Music = writer.Music.Titel });
            }
            var model = new MusicDetailViewModel()
            {
                ID = music.ID,
                Titel = music.Titel,
                ReleaseDate = music.ReleaseDate,
                Lenght = music.Lenght,
                Reviews = Reviews,
                Genres = genres,
                Directors = directors,
                Writers = writers,
                PhotoPath = music.PhotoPath,
                Youtube = music.YoutubeTrailer,
                Listened = music.Listened,
                WantToListen = music.WantToListen,
            };

            foreach (var review in music.Reviews)
            {
                totalrating = totalrating + review.Rating;
                teller++;
            }
            if (teller != 0)
            {
                ViewBag.avg = avgrating = totalrating / teller;

            }
            if (music == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Musics/Create
    [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Musics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    [Authorize]
        public async Task<IActionResult> Create(MusicViewModels model)
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

                _context.Add(new Music()
                {
                    Titel = model.Titel,
                    Lenght = model.Lenght,
                    PhotoPath = uniqueFileName,
                    Listened = model.Listened,
                    WantToListen = model.WantToListen,
                    ReleaseDate = model.ReleaseDate,
                    YoutubeTrailer = model.YoutubeTrailer,
                });
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: Musics/Edit/5
    [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var music = await _context.Musics.FindAsync(id);
            var model = new MusicViewModels()
            {
                ID = music.ID,
                Lenght = music.Lenght,
                PhotoPath = music.PhotoPath,
                ReleaseDate = music.ReleaseDate,
                Titel = music.Titel,
                WantToListen = music.WantToListen,
                Listened = music.Listened,
                YoutubeTrailer = music.YoutubeTrailer
            };
            if (music == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Musics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    [Authorize]
        public async Task<IActionResult> Edit(MusicViewModels model)
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
                var music = _context.Musics.Find(model.ID);
                music.Lenght = model.Lenght;
                music.ReleaseDate = model.ReleaseDate;
                music.Titel = model.Titel;
                music.WantToListen = model.WantToListen;
                music.Listened = model.Listened;
                music.YoutubeTrailer = model.YoutubeTrailer;
                if (model.Photo != null)
                {
                    music.PhotoPath = uniqueFileName;
                }
                _context.Update(music);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Musics/Delete/5
    [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var music = await _context.Musics
                .FirstOrDefaultAsync(m => m.ID == id);
            if (music == null)
            {
                return NotFound();
            }
            var model = new MusicViewModels()
            {
                Titel = music.Titel,
            };
            return View(model);
        }

        // POST: Musics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var music = await _context.Musics.FindAsync(id);
            _context.Musics.Remove(music);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusicExists(int id)
        {
            return _context.Musics.Any(e => e.ID == id);
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
