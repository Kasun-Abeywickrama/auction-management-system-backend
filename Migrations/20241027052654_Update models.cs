using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class Updatemodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduledEndTime",
                table: "auctionSchedules");

            migrationBuilder.DropColumn(
                name: "ScheduledTime",
                table: "Auctions");

            migrationBuilder.RenameColumn(
                name: "Images",
                table: "Products",
                newName: "ImageUrls");

            migrationBuilder.RenameColumn(
                name: "ScheduledStartTime",
                table: "auctionSchedules",
                newName: "ScheduledTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrls",
                table: "Products",
                newName: "Images");

            migrationBuilder.RenameColumn(
                name: "ScheduledTime",
                table: "auctionSchedules",
                newName: "ScheduledStartTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledEndTime",
                table: "auctionSchedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledTime",
                table: "Auctions",
                type: "datetime2",
                nullable: true);
        }
    }
}
