using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddWatchAuctionPK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WatchAuctions",
                columns: table => new
                {
                    WatchAuctionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BidId = table.Column<int>(type: "int", nullable: true),
                    AuctionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchAuctions", x => x.WatchAuctionId);
                    table.ForeignKey(
                        name: "FK_WatchAuctions_Auctions_AuctionId",
                        column: x => x.AuctionId,
                        principalTable: "Auctions",
                        principalColumn: "AuctionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WatchAuctions_Bids_BidId",
                        column: x => x.BidId,
                        principalTable: "Bids",
                        principalColumn: "BidId");
                    table.ForeignKey(
                        name: "FK_WatchAuctions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WatchAuctions_AuctionId",
                table: "WatchAuctions",
                column: "AuctionId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchAuctions_BidId",
                table: "WatchAuctions",
                column: "BidId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchAuctions_UserId",
                table: "WatchAuctions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchAuctions");
        }
    }
}
