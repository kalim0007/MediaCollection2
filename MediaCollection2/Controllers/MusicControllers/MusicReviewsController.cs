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
using Microsoft.AspNetCore.Authorization;

namespace MediaCollection2.Controllers.MusicControllers
{
    public class MusicReviewsController : Controller
    {
        private readonly MediaCollectionContext _context;

        public MusicReviewsController(MediaCollectionContext context)
        {
            _context = context;
        }

        // GET: MusicReviews
        public IActionResult Index()
        {
            var Reviews = _context.MusicReviews.Include(m => m.Musics);
            List<MusicReviewViewModel> model = new List<MusicReviewViewModel>();
            foreach (var review in Reviews)
            {
                model.Add(new MusicReviewViewModel()
                {
                    Music = review.Musics.Titel,
                    Comment = review.Comment,
                    Rating = review.Rating,
                    ID = review.ID
                });
            }
            return View(model);
        }

        // GET: MusicReviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.MusicReviews
                .Include(m => m.Musics)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (review == null)
            {
                return NotFound();
            }
            var model = (new MusicReviewViewModel()
            {
                Music = review.Musics.Titel,
                Comment = review.Comment,
                Rating = review.Rating,
                ID = review.ID
            });
            return View(model);
        }

        // GET: MusicReviews/Create
    [Authorize]
        public IActionResult Create()
        {
            ViewData["MusicsID"] = new SelectList(_context.Musics, "ID", "Titel");
            return View();
        }

        // POST: MusicReviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    [Authorize]
        public async Task<IActionResult> Create(MusicReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(new MusicReview() { Comment=model.Comment, Rating=model.Rating, MusicsID=model.MusicID });
                await _context.SaveChangesAsync();
            }
            ViewData["MusicsID"] = new SelectList(_context.Musics, "ID", "Titel");
            return RedirectToAction(nameof(Index));
        }

        // GET: MusicReviews/Edit/5
    [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.MusicReviews.FindAsync(id);
            var model = new MusicReviewViewModel()
            {
                Music = review.Musics.Titel,
                Comment = review.Comment,
                Rating = review.Rating,
                ID = review.ID
            };
            if (review == null)
            {
                return NotFound();
            }
            ViewData["MusicsID"] = new SelectList(_context.Musics, "ID", "Titel");
            return View(model);
        }

        // POST: MusicReviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    [Authorize]
        public async Task<IActionResult> Edit(MusicReviewViewModel model)
        {

            if (ModelState.IsValid)
            {
                var review = await _context.MusicReviews.FindAsync(model);
                review.Rating = model.Rating;
                review.Comment = model.Comment;
                review.MusicsID = model.MusicID;
                _context.Update(review);
                    await _context.SaveChangesAsync();

            }
            ViewData["MusicsID"] = new SelectList(_context.Musics, "ID", "Titel");
            return RedirectToAction(nameof(Index));

        }

        // GET: MusicReviews/Delete/5
    [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.MusicReviews
                .Include(m => m.Musics)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (review == null)
            {
                return NotFound();
            }
            var model = new MusicReviewViewModel()
            {
                Music = review.Musics.Titel,
                Comment = review.Comment,
                Rating = review.Rating,
                ID = review.ID
            };
            return View(model);
        }

        // POST: MusicReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
    [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var musicReview = await _context.MusicReviews.FindAsync(id);
            _context.MusicReviews.Remove(musicReview);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MusicReviewExists(int id)
        {
            return _context.MusicReviews.Any(e => e.ID == id);
        }
    }
}
