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
using Microsoft.AspNetCore.Authorization;

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

        public IActionResult Index()
        {
            var userId = userManager.GetUserId(HttpContext.User);

            List<MusicPlaylistViewModel> model = new List<MusicPlaylistViewModel>();
            foreach (var playlist in _context.MusicPlaylists.Include(m => m.User))
            {
                if (userId==playlist.UserId)
                {
                    model.Add(new MusicPlaylistViewModel()
                    {
                        ID = playlist.ID,
                        Naam = playlist.Naam,
                        UserID = userId,
                        Public = playlist.Public,
                    });
                    return View(model);
                }
                else if(playlist.Public==true)
                {
                    model.Add(new MusicPlaylistViewModel()
                    {
                        ID = playlist.ID,
                        Naam = playlist.Naam,
                        UserID = userId,
                        Public = playlist.Public,
                    });
                    return View(model);
                }


            }
            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicPlaylist = await _context.MusicPlaylists.Include(m => m.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            List<MusicViewModels> musics = new List<MusicViewModels>();
            foreach (var music in _context.MusicPlaylistCombs.Include(m => m.Musics).Where(m => m.MusicPlaylistID == id))
            {
                musics.Add(new MusicViewModels() { ID=music.Musics.ID, Titel = music.Musics.Titel, ReleaseDate = music.Musics.ReleaseDate, Lenght = music.Musics.Lenght , PhotoPath = music.Musics.PhotoPath });
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

    [Authorize]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
    [Authorize]
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

    [Authorize]
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


        [HttpPost]
        [ValidateAntiForgeryToken]
    [Authorize]
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

    [Authorize]
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
    [Authorize]
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
