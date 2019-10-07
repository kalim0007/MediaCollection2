using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaCollection2.Migrations
{
    public partial class ftffgfg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WantToListen",
                table: "Movies",
                newName: "Watched");

            migrationBuilder.RenameColumn(
                name: "Listened",
                table: "Movies",
                newName: "WantToWatch");

            migrationBuilder.AddColumn<string>(
                name: "YoutubeTrailer",
                table: "Musics",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YoutubeTrailer",
                table: "Movies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YoutubeTrailer",
                table: "Musics");

            migrationBuilder.DropColumn(
                name: "YoutubeTrailer",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "Watched",
                table: "Movies",
                newName: "WantToListen");

            migrationBuilder.RenameColumn(
                name: "WantToWatch",
                table: "Movies",
                newName: "Listened");
        }
    }
}
