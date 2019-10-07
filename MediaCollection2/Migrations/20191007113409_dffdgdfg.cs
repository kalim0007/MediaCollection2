using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaCollection2.Migrations
{
    public partial class dffdgdfg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Listened",
                table: "Movies",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "WantToListen",
                table: "Movies",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Public",
                table: "MoviePlaylists",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Listened",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "WantToListen",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Public",
                table: "MoviePlaylists");
        }
    }
}
