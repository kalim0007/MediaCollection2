﻿// <auto-generated />
using System;
using MediaCollection2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MediaCollection2.Migrations
{
    [DbContext(typeof(MediaCollectionContext))]
    partial class MediaCollectionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MediaCollection2.Domain.Director", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<int>("MovieID");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.HasIndex("MovieID");

                    b.ToTable("Directors");
                });

            modelBuilder.Entity("MediaCollection2.Domain.Genre", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MovieID");

                    b.Property<string>("Naam");

                    b.HasKey("ID");

                    b.HasIndex("MovieID");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("MediaCollection2.Domain.Movie", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Lenght");

                    b.Property<string>("PhotoPath");

                    b.Property<DateTime>("ReleaseDate");

                    b.Property<string>("Titel");

                    b.HasKey("ID");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MediaCollection2.Domain.MoviePlaylist", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naam");

                    b.Property<string>("UserId");

                    b.HasKey("ID");

                    b.HasIndex("UserId");

                    b.ToTable("MoviePlaylists");
                });

            modelBuilder.Entity("MediaCollection2.Domain.MoviePlaylistComb", b =>
                {
                    b.Property<int>("MoviePlaylistID");

                    b.Property<int>("MovieID");

                    b.HasKey("MoviePlaylistID", "MovieID");

                    b.HasIndex("MovieID");

                    b.ToTable("MoviePlaylistCombs");
                });

            modelBuilder.Entity("MediaCollection2.Domain.music.Album", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naam");

                    b.HasKey("ID");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("MediaCollection2.Domain.music.Music", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Lenght");

                    b.Property<bool>("Listened");

                    b.Property<string>("PhotoPath");

                    b.Property<DateTime>("ReleaseDate");

                    b.Property<string>("Titel");

                    b.Property<bool>("WantToListen");

                    b.HasKey("ID");

                    b.ToTable("Musics");
                });

            modelBuilder.Entity("MediaCollection2.Domain.music.MusicAlbum", b =>
                {
                    b.Property<int>("AlbumID");

                    b.Property<int>("MusicID");

                    b.HasKey("AlbumID", "MusicID");

                    b.HasIndex("MusicID");

                    b.ToTable("MusicAlbums");
                });

            modelBuilder.Entity("MediaCollection2.Domain.music.MusicDirector", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<int>("MusicID");

                    b.Property<string>("Name");

                    b.Property<string>("PhotoPath");

                    b.HasKey("ID");

                    b.HasIndex("MusicID");

                    b.ToTable("MusicDirectors");
                });

            modelBuilder.Entity("MediaCollection2.Domain.music.MusicGenre", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MusicID");

                    b.Property<string>("Naam");

                    b.HasKey("ID");

                    b.HasIndex("MusicID");

                    b.ToTable("MusicGenres");
                });

            modelBuilder.Entity("MediaCollection2.Domain.music.MusicPlaylist", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naam");

                    b.Property<string>("UserId");

                    b.HasKey("ID");

                    b.HasIndex("UserId");

                    b.ToTable("MusicPlaylists");
                });

            modelBuilder.Entity("MediaCollection2.Domain.music.MusicPlaylistComb", b =>
                {
                    b.Property<int>("MusicPlaylistID");

                    b.Property<int>("MusicID");

                    b.Property<int?>("AlbumID");

                    b.HasKey("MusicPlaylistID", "MusicID");

                    b.HasIndex("AlbumID");

                    b.HasIndex("MusicID");

                    b.ToTable("MusicPlaylistCombs");
                });

            modelBuilder.Entity("MediaCollection2.Domain.music.MusicReview", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment");

                    b.Property<int>("MusicsID");

                    b.Property<int>("Rating");

                    b.HasKey("ID");

                    b.HasIndex("MusicsID");

                    b.ToTable("MusicReviews");
                });

            modelBuilder.Entity("MediaCollection2.Domain.music.MusicWriter", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<int>("MusicID");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.HasIndex("MusicID");

                    b.ToTable("MusicWriters");
                });

            modelBuilder.Entity("MediaCollection2.Domain.Review", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment");

                    b.Property<int>("MovieID");

                    b.Property<int>("Rating");

                    b.HasKey("ID");

                    b.HasIndex("MovieID");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("MediaCollection2.Domain.Writer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<int>("MovieID");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.HasIndex("MovieID");

                    b.ToTable("Writers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MediaCollection2.Domain.Director", b =>
                {
                    b.HasOne("MediaCollection2.Domain.Movie", "Movies")
                        .WithMany("Directors")
                        .HasForeignKey("MovieID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MediaCollection2.Domain.Genre", b =>
                {
                    b.HasOne("MediaCollection2.Domain.Movie", "Movie")
                        .WithMany("Genres")
                        .HasForeignKey("MovieID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MediaCollection2.Domain.MoviePlaylist", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MediaCollection2.Domain.MoviePlaylistComb", b =>
                {
                    b.HasOne("MediaCollection2.Domain.Movie", "Movie")
                        .WithMany()
                        .HasForeignKey("MovieID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MediaCollection2.Domain.MoviePlaylist", "MoviePlaylist")
                        .WithMany("Movies")
                        .HasForeignKey("MoviePlaylistID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MediaCollection2.Domain.music.MusicAlbum", b =>
                {
                    b.HasOne("MediaCollection2.Domain.music.Album", "Album")
                        .WithMany()
                        .HasForeignKey("AlbumID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MediaCollection2.Domain.music.Music", "Musics")
                        .WithMany()
                        .HasForeignKey("MusicID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MediaCollection2.Domain.music.MusicDirector", b =>
                {
                    b.HasOne("MediaCollection2.Domain.music.Music", "Musics")
                        .WithMany("Directors")
                        .HasForeignKey("MusicID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MediaCollection2.Domain.music.MusicGenre", b =>
                {
                    b.HasOne("MediaCollection2.Domain.music.Music", "Musics")
                        .WithMany("Genres")
                        .HasForeignKey("MusicID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MediaCollection2.Domain.music.MusicPlaylist", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MediaCollection2.Domain.music.MusicPlaylistComb", b =>
                {
                    b.HasOne("MediaCollection2.Domain.music.Album")
                        .WithMany("Musics")
                        .HasForeignKey("AlbumID");

                    b.HasOne("MediaCollection2.Domain.music.Music", "Musics")
                        .WithMany()
                        .HasForeignKey("MusicID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MediaCollection2.Domain.music.MusicPlaylist", "MusicPlaylist")
                        .WithMany("Musics")
                        .HasForeignKey("MusicPlaylistID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MediaCollection2.Domain.music.MusicReview", b =>
                {
                    b.HasOne("MediaCollection2.Domain.music.Music", "Musics")
                        .WithMany("Reviews")
                        .HasForeignKey("MusicsID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MediaCollection2.Domain.music.MusicWriter", b =>
                {
                    b.HasOne("MediaCollection2.Domain.music.Music", "Musics")
                        .WithMany("Writers")
                        .HasForeignKey("MusicID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MediaCollection2.Domain.Review", b =>
                {
                    b.HasOne("MediaCollection2.Domain.Movie", "Movie")
                        .WithMany("Reviews")
                        .HasForeignKey("MovieID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MediaCollection2.Domain.Writer", b =>
                {
                    b.HasOne("MediaCollection2.Domain.Movie", "Movies")
                        .WithMany("Writers")
                        .HasForeignKey("MovieID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
