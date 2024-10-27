using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAuctuintable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentBid",
                table: "Auctions");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Auctions",
                newName: "ReturnShippingPaidBy");

            migrationBuilder.AddColumn<bool>(
                name: "AcceptReturn",
                table: "Auctions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IrregularDimension",
                table: "Auctions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSold",
                table: "Auctions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackageDimension",
                table: "Auctions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PackageWeight",
                table: "Auctions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReturnAllowedWithin",
                table: "Auctions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReturnMethod",
                table: "Auctions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ScheduledTime",
                table: "Auctions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingMethod",
                table: "Auctions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptReturn",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "IrregularDimension",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "IsSold",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "PackageDimension",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "PackageWeight",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "ReturnAllowedWithin",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "ReturnMethod",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "ScheduledTime",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "ShippingMethod",
                table: "Auctions");

            migrationBuilder.RenameColumn(
                name: "ReturnShippingPaidBy",
                table: "Auctions",
                newName: "Status");

            migrationBuilder.AddColumn<decimal>(
                name: "CurrentBid",
                table: "Auctions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
