using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediaCollection2.Data;
using MediaCollection2.Domain;
using MediaCollection2.Models.Review;
using MediaCollection2.Models.Movies;
using MediaCollection2.Models;

namespace MediaCollection2.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly MediaCollectionContext context;

        public ReviewsController(MediaCollectionContext Context)
        {
            context = Context;
        }

        // GET: Reviews
        public  IActionResult Index()
        {
            var model = new List<MovieReviewListViewModel>();
            var reviews = context.Reviews.Include(r=>r.Movie);
            foreach (var review in reviews)
            {
                model.Add(new MovieReviewListViewModel()
                {
                    ID = review.ID,
                    Rating = review.Rating,
                    Comment = review.Comment,
                    MovieID  =review.MovieID,
                    Movie = review.Movie.Titel

                });
            }

            return View(model);
        }

        public IActionResult Details(int? id)
        {
            var review = context.Reviews.Include(m => m.Movie).FirstOrDefault(m => m.ID == id);
            var movie = new CreateMovieViewModel()
            {
                ID = review.Movie.ID,
                ReleaseDate = review.Movie.ReleaseDate,
                Lenght = review.Movie.Lenght,
                Titel = review.Movie.Titel,
            };
            var model = new MovieReviewDetailsViewModel()
            {
                ID = review.ID,
                MovieID = review.MovieID,
                Rating = review.Rating,
                Comment = review.Comment,
                Movie = movie
            };

            if (movie == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // GET: Reviews/Create
        public IActionResult Create()
        {
            ViewData["MovieID"] = new SelectList(context.Movies, "ID", "Titel");
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MovieReviewCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                context.Add(new Review() { Comment=model.Comment,Rating=model.Rating,MovieID=model.MovieID });
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieID"] = new SelectList(context.Movies, "ID", "Titel", model.MovieID);
            return RedirectToAction(nameof(Index));
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await context.Reviews.FindAsync(id);
            var model = new MovieReviewEditViewModel()
            {
                Comment = review.Comment,
                Rating = review.Rating,
                MovieID = review.MovieID,
                ID = review.ID
            };
            if (review == null)
            {
                return NotFound();
            }
            ViewData["MovieID"] = new SelectList(context.Movies, "ID", "Titel", review.MovieID);
            return View(model);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  MovieReviewEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                    var review = context.Reviews.Find(id);
                    review.MovieID = model.MovieID;
                    review.Rating = model.Rating;
                    review.Comment = model.Comment;
                    review.MovieID = model.MovieID;
                    context.Update(review);
                    await context.SaveChangesAsync();
            }
            ViewData["MovieID"] = new SelectList(context.Movies, "ID", "Titel", model.MovieID);
            return RedirectToAction(nameof(Index));
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var review = await context.Reviews
                .Include(r => r.Movie)
                .FirstOrDefaultAsync(m => m.ID == id);
            var model = new MovieReviewDeleteViewModel()
            {
                ID = review.ID,
                Rating = review.Rating,
                Comment = review.Comment,
                MovieID = review.MovieID,
            };
            return View(model);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await context.Reviews.FindAsync(id);
            context.Reviews.Remove(review);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
