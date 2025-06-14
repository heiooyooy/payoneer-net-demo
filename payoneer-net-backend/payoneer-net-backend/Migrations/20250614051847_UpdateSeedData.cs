using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace payoneer_net_backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("deadbeef-dead-beef-dead-beef00000001"));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "OrderId", "ProductId" },
                values: new object[] { new Guid("a5c2cb45-0217-4057-acf7-2da1d500a396"), new Guid("d3ffbd6f-4fc8-412c-bcd9-e3a4b4b25632") });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "OrderId", "ProductId" },
                values: new object[] { new Guid("a5c2cb45-0217-4057-acf7-2da1d500a396"), new Guid("58340ce8-ed4e-4519-ab56-98c72f8e7524") });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CreatedAt", "CustomerName" },
                values: new object[] { new Guid("a5c2cb45-0217-4057-acf7-2da1d500a396"), new DateTime(2025, 6, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed Customer" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("a5c2cb45-0217-4057-acf7-2da1d500a396"));

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "OrderId", "ProductId" },
                values: new object[] { new Guid("deadbeef-dead-beef-dead-beef00000001"), new Guid("deadbeef-dead-beef-dead-beef00000002") });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "OrderId", "ProductId" },
                values: new object[] { new Guid("deadbeef-dead-beef-dead-beef00000001"), new Guid("deadbeef-dead-beef-dead-beef00000003") });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CreatedAt", "CustomerName" },
                values: new object[] { new Guid("deadbeef-dead-beef-dead-beef00000001"), new DateTime(2025, 6, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Seed Customer" });
        }
    }
}
