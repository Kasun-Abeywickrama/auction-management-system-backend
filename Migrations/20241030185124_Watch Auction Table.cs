using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class WatchAuctionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchAuctions_Users_UserId",
                table: "WatchAuctions");

            migrationBuilder.DropIndex(
                name: "IX_WatchAuctions_UserId",
                table: "WatchAuctions");

            migrationBuilder.CreateTable(
                name: "UserWatchAuctions",
                columns: table => new
                {
                    UsersUserId = table.Column<int>(type: "int", nullable: false),
                    WatchAuctionsWatchAuctionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWatchAuctions", x => new { x.UsersUserId, x.WatchAuctionsWatchAuctionId });
                    table.ForeignKey(
                        name: "FK_UserWatchAuctions_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWatchAuctions_WatchAuctions_WatchAuctionsWatchAuctionId",
                        column: x => x.WatchAuctionsWatchAuctionId,
                        principalTable: "WatchAuctions",
                        principalColumn: "WatchAuctionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserWatchAuctions_WatchAuctionsWatchAuctionId",
                table: "UserWatchAuctions",
                column: "WatchAuctionsWatchAuctionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWatchAuctions");

            migrationBuilder.CreateIndex(
                name: "IX_WatchAuctions_UserId",
                table: "WatchAuctions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WatchAuctions_Users_UserId",
                table: "WatchAuctions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
