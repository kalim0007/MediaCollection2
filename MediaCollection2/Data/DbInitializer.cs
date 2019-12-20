using MediaCollection2.Domain;
using MediaCollection2.Domain.music;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MediaCollectionContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            //context.Database.EnsureCreated();

            // Look for any students.
            if (context.Movies.Any())
            {
                return;   // DB has been seeded
            }
            IdentityUser user = new IdentityUser
            {
                UserName = "MediaTestAccount@hotmail.com",
                Email = "MediaTestAccount@hotmail.com"
            };

            IdentityResult result = userManager.CreateAsync(user, "12Abcd.").Result;

            IdentityRole role = new IdentityRole
            {
                Name = "Admin",
            };
             roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Admin").Wait();
            }
            var movies = new Movie[]
            {
                new Movie
                {
                    Titel = "The IrishMan",
                    Lenght = 209,
                    PhotoPath = "TheIrishMan.jpg",
                    ReleaseDate = DateTime.Parse("2019-11-27"),
                    Watched = false,
                    WantToWatch =false,
                    YoutubeTrailer = "https://www.youtube.com/watch?v=WHXxVmeGQUc"
                },
                new Movie
                {
                    Titel = "Mariage Story",
                    Lenght = 197,
                    PhotoPath = "MariageStory.jpg",
                    ReleaseDate = DateTime.Parse("2019-12-06"),
                    Watched = false,
                    WantToWatch =false,
                    YoutubeTrailer = "https://www.youtube.com/watch?v=BHi-a1n8t7M"
                },

                new Movie
                {
                    Titel = "Wonder Woman 1984",
                    Lenght = 197,
                    PhotoPath = "WonderWoman.jpg",
                    ReleaseDate = DateTime.Parse("2020-06-05"),
                    Watched = false,
                    WantToWatch =false,
                    YoutubeTrailer = "https://www.youtube.com/watch?v=sfM7_JLk-84"
                },

                new Movie
                {
                    Titel = "Knives Out",
                    Lenght = 190,
                    PhotoPath = "KnivesOut.jpg",
                    ReleaseDate = DateTime.Parse("2019-06-05"),
                    Watched = false,
                    WantToWatch =false,
                    YoutubeTrailer = "https://www.youtube.com/watch?v=xi-1NchUqMA"
                },

                new Movie
                {
                    Titel = "Jumanji: The Next Level",
                    Lenght = 197,
                    PhotoPath = "Jumanji.jpg",
                    ReleaseDate = DateTime.Parse("2020-06-05"),
                    Watched = false,
                    WantToWatch =false,
                    YoutubeTrailer = "https://www.youtube.com/watch?v=rBxcF-r9Ibs"
                },

            };
            foreach (var movie in movies)
            {
                context.Movies.Add(movie);
            }
            context.SaveChanges();

            var directors = new Director[]
            {
                new Director
                {
                    Name="Martin Scorsese",
                    DateOfBirth = DateTime.Parse("1942-11-17"),
                    MovieID = movies[0].ID
                },

                new Director
                {
                    Name="Noah Baumbach",
                    DateOfBirth = DateTime.Parse("1969-09-3"),
                    MovieID = movies[1].ID
                },

                new Director
                {
                    Name="Patty Jenkins",
                    DateOfBirth = DateTime.Parse("1971-07-24"),
                    MovieID = movies[2].ID
                },

                new Director
                {
                    Name="Patty Jenkins",
                    DateOfBirth = DateTime.Parse("1971-07-24"),
                    MovieID = movies[3].ID
                },

                new Director
                {
                    Name="Patty Jenkins",
                    DateOfBirth = DateTime.Parse("1971-07-24"),
                    MovieID = movies[4].ID
                },
            };
            foreach (var director in directors)
            {
                context.Directors.Add(director);
            }
            context.SaveChanges();
            var writers = new Writer[]
{
                new Writer
                {
                    Name="Steven Zailian",
                    DateOfBirth = DateTime.Parse("1953-01-30"),
                    MovieID = movies[0].ID
                },

                new Writer
                {
                    Name="Noah Baumbach",
                    DateOfBirth = DateTime.Parse("1969-09-3"),
                    MovieID = movies[1].ID
                },

                new Writer
                {
                    Name="Geoff Johns",
                    DateOfBirth = DateTime.Parse("1973-01-25"),
                    MovieID = movies[2].ID
                },

                new Writer
                {
                    Name="Geoff Johns",
                    DateOfBirth = DateTime.Parse("1973-01-25"),
                    MovieID = movies[3].ID
                },

                new Writer
                {
                    Name="Geoff Johns",
                    DateOfBirth = DateTime.Parse("1973-01-25"),
                    MovieID = movies[4].ID
                },
};
            foreach (var writer in writers)
            {
                context.Writers.Add(writer);
            }
            context.SaveChanges();
            var genres = new Genre[]
{
                new Genre
                {
                    Naam="Biography",
                    MovieID = movies[0].ID
                },
                new Genre
                {
                    Naam="Crime",
                    MovieID = movies[1].ID

                },
                new Genre
                {
                    Naam="Fantasy",
                    MovieID = movies[2].ID
                },

                new Genre
                {
                    Naam="Comedy",
                    MovieID = movies[3].ID
                },

                new Genre
                {
                    Naam="Action",
                    MovieID = movies[4].ID
                },
};
            foreach (var genre in genres)
            {
                context.Genres.Add(genre);
            }
            context.SaveChanges();
            var reviews = new Review[]
{
                new Review
                {
                    Comment = "Outstanding Movie",
                    Rating = 10,
                    MovieID = movies[0].ID,

                },

                new Review
                {
                    Comment = "Outstanding Movie",
                    Rating = 9,
                    MovieID = movies[1].ID,
                },

                new Review
                {
                    Comment = "Outstanding Movie",
                    Rating = 8,
                    MovieID = movies[2].ID,
                },

                new Review
                {
                    Comment = "Outstanding Movie",
                    Rating = 7,
                    MovieID = movies[3].ID,
                },

                new Review
                {
                    Comment = "Outstanding Movie",
                    Rating = 6,
                    MovieID = movies[4].ID,
                },
};
            foreach (var review in reviews)
            {
                context.Reviews.Add(review);
            }
            context.SaveChanges();
            AdjustMovie(movies, directors, writers, genres, reviews,0);
            AdjustMovie(movies, directors, writers, genres, reviews,1);
            AdjustMovie(movies, directors, writers, genres, reviews,2);
            AdjustMovie(movies, directors, writers, genres, reviews,3);
            AdjustMovie(movies, directors, writers, genres, reviews,4);
            context.SaveChanges();

            var musics = new Music[]
            {
                new Music
                {
                    Titel = "Happier",
                    Lenght = 5,
                    Listened = true,
                    ReleaseDate = DateTime.Parse("2018-01-01"),
                    PhotoPath = "Happier.jpg",
                    YoutubeTrailer="https://www.youtube.com/watch?v=QgKYZWRH4DA",
                    WantToListen = true,
                },

                new Music
                {
                    Titel = "I Am A Mess",
                    Lenght = 5,
                    Listened = true,
                    ReleaseDate = DateTime.Parse("2018-01-01"),
                    PhotoPath = "IamaMess.jpg",
                    YoutubeTrailer="https://www.youtube.com/watch?v=LdH7aFjDzjI",
                    WantToListen = true,
                },

                new Music
                {
                    Titel = "Let You Love Me",
                    Lenght = 5,
                    Listened = true,
                    ReleaseDate = DateTime.Parse("2018-01-01"),
                    PhotoPath = "LetYouLoveMe.jpg",
                    YoutubeTrailer="https://www.youtube.com/watch?v=XCQK6LmhYqc",
                    WantToListen = true,
                },

                new Music
                {
                    Titel = "Perfect",
                    Lenght = 5,
                    Listened = true,
                    ReleaseDate = DateTime.Parse("2018-01-01"),
                    PhotoPath = "Perfect.jpg",
                    YoutubeTrailer="https://www.youtube.com/watch?v=iKzRIweSBLA",
                    WantToListen = true,
                },

                new Music
                {
                    Titel = "Sorry",
                    Lenght = 5,
                    Listened = true,
                    ReleaseDate = DateTime.Parse("2018-01-01"),
                    PhotoPath = "Sorry.jpg",
                    YoutubeTrailer="https://www.youtube.com/watch?v=fRh_vgS2dFE",
                    WantToListen = true,
                },

            };
            foreach (var music in musics)
            {
                context.Musics.Add(music);
            }
            context.SaveChanges();
            var musicwriters = new MusicWriter[]
            {
                new MusicWriter
                {
                    Name="Marshmello",
                    DateOfBirth = DateTime.Parse("1992-05-19"),
                    PhotoPath ="",
                    MusicID = musics[0].ID,
                },

                new MusicWriter
                {
                    Name="Bebe Rexha",
                    DateOfBirth = DateTime.Parse("1992-05-19"),
                    PhotoPath ="IamaMess.jpg",
                    MusicID = musics[1].ID,
                },

                new MusicWriter
                {
                    Name="Rita Ora",
                    DateOfBirth = DateTime.Parse("1992-05-19"),
                    PhotoPath ="RitaOra.jpg",
                    MusicID = musics[2].ID,
                },

                new MusicWriter
                {
                    Name="Ed Sheeran",
                    DateOfBirth = DateTime.Parse("1992-05-19"),
                    PhotoPath ="Edsheran.jpg",
                    MusicID = musics[3].ID,
                },

                new MusicWriter
                {
                    Name="Justin Bieber",
                    DateOfBirth = DateTime.Parse("1992-05-19"),
                    PhotoPath ="JustinBieber.jpg",
                    MusicID = musics[4].ID,
                },
            };
            foreach (var writer in musicwriters)
            {
                context.MusicWriters.Add(writer);
            }
            context.SaveChanges();
            var musicgenres = new MusicGenre[]
{
                new MusicGenre
                {
                    Naam="hiphop",
                    MusicID = musics[0].ID
                },
                new MusicGenre
                {
                    Naam="hiphop",
                    MusicID = musics[1].ID

                },
                new MusicGenre
                {
                    Naam="hiphop",
                    MusicID = musics[2].ID
                },

                new MusicGenre
                {
                    Naam="hiphop",
                    MusicID = musics[3].ID
                },

                new MusicGenre
                {
                    Naam="hiphop",
                    MusicID = musics[4].ID
                },
};
            foreach (var genre in musicgenres)
            {
                context.MusicGenres.Add(genre);
            }
            context.SaveChanges();
            var musicreviews = new MusicReview[]
{
                new MusicReview
                {
                    Comment = "Outstanding ",
                    Rating = 10,
                    MusicsID = musics[0].ID,

                },

                new MusicReview
                {
                    Comment = "Outstanding ",
                    Rating = 9,
                    MusicsID = musics[1].ID,
                },

                new MusicReview
                {
                    Comment = "Outstanding ",
                    Rating = 8,
                    MusicsID = musics[2].ID,
                },

                new MusicReview
                {
                    Comment = "Outstanding ",
                    Rating = 7,
                    MusicsID = musics[3].ID,
                },

                new MusicReview
                {
                    Comment = "Outstanding ",
                    Rating = 6,
                    MusicsID = musics[4].ID,
                },
};
            foreach (var review in musicreviews)
            {
                context.MusicReviews.Add(review);
            }
            context.SaveChanges();
            Adjustmusic(musics,  musicwriters, musicgenres, musicreviews, 0);
            Adjustmusic(musics,  musicwriters, musicgenres, musicreviews, 1);
            Adjustmusic(musics,  musicwriters, musicgenres, musicreviews, 2);
            Adjustmusic(musics,  musicwriters, musicgenres, musicreviews, 3);
            Adjustmusic(musics,  musicwriters, musicgenres, musicreviews, 4);
            context.SaveChanges();

        }

        private static void AdjustMovie(Movie[] movies, Director[] directors, Writer[] writers, Genre[] genres, Review[] reviews, int index)
        {
            movies[index].Directors.Add(directors[index]);
            movies[index].Writers.Add(writers[index]);
            movies[index].Reviews.Add(reviews[index]);
            movies[index].Genres.Add(genres[index]);
        }
        private static void Adjustmusic(Music[] musics,  MusicWriter[] writers, MusicGenre[] genres, MusicReview[] reviews, int index)
        {
            musics[index].Writers.Add(writers[index]);
            musics[index].Reviews.Add(reviews[index]);
            musics[index].Genres.Add(genres[index]);
        }
    }
}
