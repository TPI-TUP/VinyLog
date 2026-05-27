using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregaArtistNameAAlbum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdArtist",
                table: "Albums");

            migrationBuilder.AddColumn<string>(
                name: "ArtistName",
                table: "Albums",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArtistName",
                table: "Albums");

            migrationBuilder.AddColumn<int>(
                name: "IdArtist",
                table: "Albums",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
