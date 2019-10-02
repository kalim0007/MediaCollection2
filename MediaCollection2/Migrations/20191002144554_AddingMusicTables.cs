using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaCollection2.Migrations
{
    public partial class AddingMusicTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naam = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MusicPlaylists",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naam = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicPlaylists", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MusicPlaylists_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Musics",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Titel = table.Column<string>(nullable: true),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    Lenght = table.Column<int>(nullable: false),
                    PhotoPath = table.Column<string>(nullable: true),
                    WantToListen = table.Column<bool>(nullable: false),
                    Listened = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musics", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MusicAlbums",
                columns: table => new
                {
                    AlbumID = table.Column<int>(nullable: false),
                    MusicID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicAlbums", x => new { x.AlbumID, x.MusicID });
                    table.ForeignKey(
                        name: "FK_MusicAlbums_Albums_AlbumID",
                        column: x => x.AlbumID,
                        principalTable: "Albums",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicAlbums_Musics_MusicID",
                        column: x => x.MusicID,
                        principalTable: "Musics",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusicDirectors",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    PhotoPath = table.Column<string>(nullable: true),
                    MusicID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicDirectors", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MusicDirectors_Musics_MusicID",
                        column: x => x.MusicID,
                        principalTable: "Musics",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusicGenres",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naam = table.Column<string>(nullable: true),
                    MusicID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicGenres", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MusicGenres_Musics_MusicID",
                        column: x => x.MusicID,
                        principalTable: "Musics",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusicPlaylistCombs",
                columns: table => new
                {
                    MusicPlaylistID = table.Column<int>(nullable: false),
                    MusicID = table.Column<int>(nullable: false),
                    AlbumID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicPlaylistCombs", x => new { x.MusicPlaylistID, x.MusicID });
                    table.ForeignKey(
                        name: "FK_MusicPlaylistCombs_Albums_AlbumID",
                        column: x => x.AlbumID,
                        principalTable: "Albums",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MusicPlaylistCombs_Musics_MusicID",
                        column: x => x.MusicID,
                        principalTable: "Musics",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicPlaylistCombs_MusicPlaylists_MusicPlaylistID",
                        column: x => x.MusicPlaylistID,
                        principalTable: "MusicPlaylists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusicReviews",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Rating = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    MusicsID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicReviews", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MusicReviews_Musics_MusicsID",
                        column: x => x.MusicsID,
                        principalTable: "Musics",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusicWriters",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    MusicID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicWriters", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MusicWriters_Musics_MusicID",
                        column: x => x.MusicID,
                        principalTable: "Musics",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MusicAlbums_MusicID",
                table: "MusicAlbums",
                column: "MusicID");

            migrationBuilder.CreateIndex(
                name: "IX_MusicDirectors_MusicID",
                table: "MusicDirectors",
                column: "MusicID");

            migrationBuilder.CreateIndex(
                name: "IX_MusicGenres_MusicID",
                table: "MusicGenres",
                column: "MusicID");

            migrationBuilder.CreateIndex(
                name: "IX_MusicPlaylistCombs_AlbumID",
                table: "MusicPlaylistCombs",
                column: "AlbumID");

            migrationBuilder.CreateIndex(
                name: "IX_MusicPlaylistCombs_MusicID",
                table: "MusicPlaylistCombs",
                column: "MusicID");

            migrationBuilder.CreateIndex(
                name: "IX_MusicPlaylists_UserId",
                table: "MusicPlaylists",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicReviews_MusicsID",
                table: "MusicReviews",
                column: "MusicsID");

            migrationBuilder.CreateIndex(
                name: "IX_MusicWriters_MusicID",
                table: "MusicWriters",
                column: "MusicID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicAlbums");

            migrationBuilder.DropTable(
                name: "MusicDirectors");

            migrationBuilder.DropTable(
                name: "MusicGenres");

            migrationBuilder.DropTable(
                name: "MusicPlaylistCombs");

            migrationBuilder.DropTable(
                name: "MusicReviews");

            migrationBuilder.DropTable(
                name: "MusicWriters");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "MusicPlaylists");

            migrationBuilder.DropTable(
                name: "Musics");
        }
    }
}
