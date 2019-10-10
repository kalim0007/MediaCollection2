using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediaCollection2.Data;
using MediaCollection2.Domain;
using MediaCollection2.Models.MoviePlaylist;
using MediaCollection2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace MediaCollection2.Controllers
{
    public class MoviePlaylistsController : Controller
    {
        private readonly MediaCollectionContext _context;
        private readonly UserManager<IdentityUser> userManager;

        public MoviePlaylistsController(MediaCollectionContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: MoviePlaylists
        public IActionResult Index()
        {
            List<MoviePlaylistViewModel> model = new List<MoviePlaylistViewModel>();

            var userId = userManager.GetUserId(HttpContext.User);
            foreach (var playlist in _context.MoviePlaylists)
            {
                if (playlist.UserId==userId)
                {
                    model.Add(new MoviePlaylistViewModel()
                    {
                        ID = playlist.ID,
                        Naam = playlist.Naam,
                    });
                    return View(model);
                }
                else if (playlist.Public)
                {
                    model.Add(new MoviePlaylistViewModel()
                    {
                        ID = playlist.ID,
                        Naam = playlist.Naam,
                    });
                }
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

            var moviePlaylist = await _context.MoviePlaylists
                .FirstOrDefaultAsync(m => m.ID == id);
            List<DeleteMovieViewModel> movies = new List<DeleteMovieViewModel>();
            foreach (var movie in _context.MoviePlaylistCombs.Include(m=>m.Movie).Where(m=>m.MoviePlaylistID==id))
            {
                movies.Add(new DeleteMovieViewModel() { ID = movie.Movie.ID, Titel = movie.Movie.Titel, ReleaseDate = movie.Movie.ReleaseDate,
                Lenght = movie.Movie.Lenght, PhotoPath = movie.Movie.PhotoPath });
            }
            if (moviePlaylist == null)
            {
                return NotFound();
            }
            var model = new MoviePlaylistViewModel()
            {
                ID = moviePlaylist.ID,
                Naam = moviePlaylist.Naam,
                Movies = movies,
            };
            return View(model);
        }

        // GET: MoviePlaylists/Create
    [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: MoviePlaylists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    [Authorize]
        public async Task<IActionResult> Create(MoviePlaylistViewModel model)
        {
            var userId = userManager.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {
                _context.Add(new MoviePlaylist() { ID = model.ID, Naam = model.Naam, UserId = userId }); ;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: MoviePlaylists/Edit/5
    [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moviePlaylist = await _context.MoviePlaylists.FindAsync(id);
            if (moviePlaylist == null)
            {
                return NotFound();
            }
            var model = new MoviePlaylistViewModel()
            {
                ID = moviePlaylist.ID,
                Naam = moviePlaylist.Naam,
            };
            return View(model);
        }

        // POST: MoviePlaylists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    [Authorize]
        public async Task<IActionResult> Edit(MoviePlaylistViewModel model)
        {
            if (ModelState.IsValid)
            {

                    var moviePlaylist = _context.MoviePlaylists.Find(model.ID);
                    moviePlaylist.Naam = model.Naam;
                    _context.Update(moviePlaylist);
                    await _context.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));

        }

        // GET: MoviePlaylists/Delete/5
    [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moviePlaylist = await _context.MoviePlaylists
                .FirstOrDefaultAsync(m => m.ID == id);

            if (moviePlaylist == null)
            {
                return NotFound();
            }
            var model = new MoviePlaylistViewModel()
            {
                ID = moviePlaylist.ID,
                Naam = moviePlaylist.Naam,
            };
            return View(model);
        }

        // POST: MoviePlaylists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
    [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moviePlaylist = await _context.MoviePlaylists.FindAsync(id);
            _context.MoviePlaylists.Remove(moviePlaylist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
