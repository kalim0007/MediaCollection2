using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaCollection2.Migrations
{
    public partial class fdsfsdfstgs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "YoutubeTrailer",
                table: "Seasons",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YoutubeTrailer",
                table: "Seasons");
        }
    }
}
