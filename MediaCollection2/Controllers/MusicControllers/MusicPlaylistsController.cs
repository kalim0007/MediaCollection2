using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediaCollection2.Data;
using MediaCollection2.Domain.music;
using Microsoft.AspNetCore.Identity;
using MediaCollection2.Models.MusicModels.MusicPlaylist;
using MediaCollection2.Models.MusicModels;

namespace MediaCollection2.Controllers.MusicControllers
{
    public class MusicPlaylistsController : Controller
    {
        private readonly MediaCollectionContext _context;
        private readonly UserManager<IdentityUser> userManager;

        public MusicPlaylistsController(MediaCollectionContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: MusicPlaylists
        public IActionResult Index()
        {
            List<MusicPlaylistViewModel> model = new List<MusicPlaylistViewModel>();
            foreach (var playlist in _context.MusicPlaylists.Include(m => m.User))
            {
                var username = "No User";
                if (playlist.User != null)
                {
                    username = playlist.User.UserName;
                }
                model.Add(new MusicPlaylistViewModel()
                {
                    ID = playlist.ID,
                    Naam = playlist.Naam,
                    User = username,
                    Public = playlist.Public,
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

            var musicPlaylist = await _context.MusicPlaylists.Include(m => m.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            List<MusicViewModels> musics = new List<MusicViewModels>();
            foreach (var movie in _context.MusicPlaylistCombs.Include(m => m.Musics).Where(m => m.MusicPlaylistID == id))
            {
                musics.Add(new MusicViewModels() { Titel = movie.Musics.Titel, ReleaseDate = movie.Musics.ReleaseDate, Lenght = movie.Musics.Lenght });
            }
            if (musicPlaylist == null)
            {
                return NotFound();
            }
            var model = new MusicPlaylistViewModel()
            {
                ID = musicPlaylist.ID,
                Naam = musicPlaylist.Naam,
                Public = musicPlaylist.Public,
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
        public async Task<IActionResult> Create(MusicPlaylistViewModel model)
        {
            var userId = userManager.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {
                _context.Add(new MusicPlaylist() { Naam = model.Naam, Public= model.Public, UserId =userId });
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

            var musicPlaylist = await _context.MusicPlaylists.Include(m => m.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (musicPlaylist == null)
            {
                return NotFound();
            }
            var model = new MusicPlaylistViewModel()
            {
                ID = musicPlaylist.ID,
                Naam = musicPlaylist.Naam,
                Public = musicPlaylist.Public,
            };
            return View(model);
        }

        // POST: MoviePlaylists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MusicPlaylistViewModel model)
        {
            if (ModelState.IsValid)
            {

                var musicPlaylist = await _context.MusicPlaylists.Include(m => m.User)
                .FirstOrDefaultAsync(m => m.ID == model.ID);
                musicPlaylist.Naam = model.Naam;
                musicPlaylist.Public = model.Public;
                _context.Update(musicPlaylist);
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

            var musicPlaylist = await _context.MusicPlaylists.Include(m => m.User)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (musicPlaylist == null)
            {
                return NotFound();
            }
            var model = new MusicPlaylistViewModel()
            {
                ID = musicPlaylist.ID,
                Naam = musicPlaylist.Naam,
            };
            return View(model);
        }

        // POST: MoviePlaylists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var musicPlaylist = await _context.MusicPlaylists.Include(m => m.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            _context.MusicPlaylists.Remove(musicPlaylist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
