using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace payoneer_net_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CreatedAt", "CustomerName" },
                values: new object[] { new Guid("deadbeef-dead-beef-dead-beef00000001"), new DateTime(2025, 6, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed Customer" });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "OrderId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, new Guid("deadbeef-dead-beef-dead-beef00000001"), new Guid("deadbeef-dead-beef-dead-beef00000002"), 10 },
                    { 2, new Guid("deadbeef-dead-beef-dead-beef00000001"), new Guid("deadbeef-dead-beef-dead-beef00000003"), 20 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("deadbeef-dead-beef-dead-beef00000001"));
        }
    }
}
