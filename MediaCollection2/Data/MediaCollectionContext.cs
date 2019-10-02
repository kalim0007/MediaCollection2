using MediaCollection2.Domain;
using MediaCollection2.Domain.music;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaCollection2.Data
{
    public class MediaCollectionContext : IdentityDbContext
    {
        public MediaCollectionContext(DbContextOptions Options) : base(Options)
        {

        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<MoviePlaylist> MoviePlaylists { get; set; }
        public DbSet<MoviePlaylistComb> MoviePlaylistCombs { get; set; }

        public DbSet<Music> Musics { get; set; }
        public DbSet<MusicGenre> MusicGenres { get; set; }
        public DbSet<MusicReview> MusicReviews { get; set; }
        public DbSet<MusicDirector> MusicDirectors { get; set; }
        public DbSet<MusicWriter> MusicWriters { get; set; }
        public DbSet<MusicPlaylist> MusicPlaylists { get; set; }
        public DbSet<MusicPlaylistComb> MusicPlaylistCombs { get; set; }

        public DbSet<MusicAlbum> MusicAlbums { get; set; }
        public DbSet<Album> Albums { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<MoviePlaylistComb>()
                .HasKey(c => new { c.MoviePlaylistID, c.MovieID });
            builder.Entity<MusicPlaylistComb>()
                .HasKey(c => new { c.MusicPlaylistID, c.MusicID });
            builder.Entity<MusicAlbum>()
    .HasKey(c => new { c.AlbumID, c.MusicID });
            base.OnModelCreating(builder);
        }
    }
}
