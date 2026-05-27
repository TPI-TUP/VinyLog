using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Albums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Albums",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "YoutubeVideoId",
                table: "Albums",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YoutubeVideoId",
                table: "Albums");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Albums",
                newName: "Title");
        }
    }
}
