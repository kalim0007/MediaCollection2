using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediaCollection2.Models;
using MediaCollection2.Models.Movies;
using MediaCollection2.Models.MusicModels;
using MediaCollection2.Models.SeriesModels;
using MediaCollection2.Data;
using Microsoft.EntityFrameworkCore;
using MediaCollection2.Models.Home;

namespace MediaCollection2.Controllers
{
    public class HomeController : Controller
    {
        private readonly MediaCollectionContext context;

        public HomeController(MediaCollectionContext context)
        {
            this.context = context;
        }
        public IActionResult MyIndex()
        {
            List<ListMovieViewModel> Movies = new List<ListMovieViewModel>();
            List<MusicViewModels> Musics = new List<MusicViewModels>();
            List<SerieViewModel> Series = new List<SerieViewModel>();
            //movies list
            var movies = context.Movies.Include(m => m.Reviews).ToList();
            movies.OrderBy(m => m.Reviews.Select(r => r.Rating));
            if (movies.Count() < 5)
            {
                for (int i = 0; i < movies.Count(); i++)
                {
                    var movieRating = 0;
                    var totalRating = 0;
                    var reviewCounter = 0;
                    if (movies[i].Reviews.Count != 0)
                    {
                        foreach (var review in movies[i].Reviews)
                        {
                            reviewCounter++;
                            totalRating += review.Rating;
                        }
                        movieRating = totalRating / reviewCounter;
                    }

                    Movies.Add(new ListMovieViewModel() { Titel = movies[i].Titel, Rating = movieRating, PhotoPath = movies[i].PhotoPath, Lenght = movies[i].Lenght, ID = movies[i].ID, YoutubeTrailer = movies[i].YoutubeTrailer, ReleaseDate = movies[i].ReleaseDate, });
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    var movieRating = 0;
                    var totalRating = 0;
                    var reviewCounter = 0;
                    if (movies[i].Reviews.Count != 0)
                    {
                        foreach (var review in movies[i].Reviews)
                        {
                            reviewCounter++;
                            totalRating += review.Rating;
                        }
                        movieRating = totalRating / reviewCounter;
                    }

                    Movies.Add(new ListMovieViewModel() { Titel = movies[i].Titel, Rating = movieRating, PhotoPath = movies[i].PhotoPath, Lenght = movies[i].Lenght, ID = movies[i].ID, YoutubeTrailer = movies[i].YoutubeTrailer, ReleaseDate = movies[i].ReleaseDate, });
                }
            }
            //musiclist
            var musics = context.Musics.Include(m => m.Reviews).ToList();
            musics.OrderBy(m => m.Reviews.Select(r => r.Rating));
            if (musics.Count() < 5)
            {
                for (int i = 0; i < musics.Count(); i++)
                {
                    var musicrating = 0;
                    var totalRating = 0;
                    var reviewCounter = 0;
                    if (musics[i].Reviews.Count != 0)
                    {
                        foreach (var review in musics[i].Reviews)
                        {
                            reviewCounter++;
                            totalRating += review.Rating;
                        }
                        musicrating = totalRating / reviewCounter;
                    }

                    Musics.Add(new MusicViewModels() { Titel = musics[i].Titel, Rating = musicrating, PhotoPath = musics[i].PhotoPath, Lenght = musics[i].Lenght, ID = musics[i].ID, YoutubeTrailer = musics[i].YoutubeTrailer, ReleaseDate = musics[i].ReleaseDate, });
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    var musicRating = 0;
                    var totalRating = 0;
                    var reviewCounter = 0;
                    if (musics[i].Reviews.Count != 0)
                    {
                        foreach (var review in musics[i].Reviews)
                        {
                            reviewCounter++;
                            totalRating += review.Rating;
                        }
                        musicRating = totalRating / reviewCounter;
                    }

                    Musics.Add(new MusicViewModels() { Titel = musics[i].Titel, Rating = musicRating, PhotoPath = musics[i].PhotoPath, Lenght = musics[i].Lenght, ID = musics[i].ID, YoutubeTrailer = musics[i].YoutubeTrailer, ReleaseDate = musics[i].ReleaseDate, });
                }
            }


            //Series List
            var series = context.Seasons.Include(m => m.Serie).ToList();
            series.OrderBy(r => r.Rating);
            if (series.Count() < 5)
            {
                for (int i = 0; i < series.Count(); i++)
                {
                    Series.Add(new SerieViewModel() { Titel = series[i].Titel, Rating = series[i].Rating, PhotoPath = series[i].PhotoPath, SeasonID = series[i].ID, YoutubeTrailer = series[i].YoutubeTrailer, SeasonNr = series[i].Nr });
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    Series.Add(new SerieViewModel() { Titel = series[i].Titel, Rating = series[i].Rating, PhotoPath = series[i].PhotoPath, SeasonID = series[i].ID, YoutubeTrailer = series[i].YoutubeTrailer, SeasonNr = series[i].Nr });
                }
            }
            var model = new MyIndexViewModel() { Movies = Movies, Musics = Musics, Series = Series };
            return View(model);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
