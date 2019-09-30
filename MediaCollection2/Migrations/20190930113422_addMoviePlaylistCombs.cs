using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaCollection2.Migrations
{
    public partial class addMoviePlaylistCombs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviePlaylistComb_Movies_MovieID",
                table: "MoviePlaylistComb");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviePlaylistComb_MoviePlaylists_MoviePlaylistID",
                table: "MoviePlaylistComb");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MoviePlaylistComb",
                table: "MoviePlaylistComb");

            migrationBuilder.RenameTable(
                name: "MoviePlaylistComb",
                newName: "MoviePlaylistCombs");

            migrationBuilder.RenameIndex(
                name: "IX_MoviePlaylistComb_MoviePlaylistID",
                table: "MoviePlaylistCombs",
                newName: "IX_MoviePlaylistCombs_MoviePlaylistID");

            migrationBuilder.RenameIndex(
                name: "IX_MoviePlaylistComb_MovieID",
                table: "MoviePlaylistCombs",
                newName: "IX_MoviePlaylistCombs_MovieID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MoviePlaylistCombs",
                table: "MoviePlaylistCombs",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePlaylistCombs_Movies_MovieID",
                table: "MoviePlaylistCombs",
                column: "MovieID",
                principalTable: "Movies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePlaylistCombs_MoviePlaylists_MoviePlaylistID",
                table: "MoviePlaylistCombs",
                column: "MoviePlaylistID",
                principalTable: "MoviePlaylists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoviePlaylistCombs_Movies_MovieID",
                table: "MoviePlaylistCombs");

            migrationBuilder.DropForeignKey(
                name: "FK_MoviePlaylistCombs_MoviePlaylists_MoviePlaylistID",
                table: "MoviePlaylistCombs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MoviePlaylistCombs",
                table: "MoviePlaylistCombs");

            migrationBuilder.RenameTable(
                name: "MoviePlaylistCombs",
                newName: "MoviePlaylistComb");

            migrationBuilder.RenameIndex(
                name: "IX_MoviePlaylistCombs_MoviePlaylistID",
                table: "MoviePlaylistComb",
                newName: "IX_MoviePlaylistComb_MoviePlaylistID");

            migrationBuilder.RenameIndex(
                name: "IX_MoviePlaylistCombs_MovieID",
                table: "MoviePlaylistComb",
                newName: "IX_MoviePlaylistComb_MovieID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MoviePlaylistComb",
                table: "MoviePlaylistComb",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePlaylistComb_Movies_MovieID",
                table: "MoviePlaylistComb",
                column: "MovieID",
                principalTable: "Movies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MoviePlaylistComb_MoviePlaylists_MoviePlaylistID",
                table: "MoviePlaylistComb",
                column: "MoviePlaylistID",
                principalTable: "MoviePlaylists",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
