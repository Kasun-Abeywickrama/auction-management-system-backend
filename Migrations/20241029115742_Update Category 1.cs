using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCategory1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Categories",
                newName: "ImageUrls");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrls",
                table: "Categories",
                newName: "ImageUrl");
        }
    }
}
