using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerce_app.Migrations
{
    /// <inheritdoc />
    public partial class show4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01dce6f1-9673-4c1b-960c-4544ec292f7e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5127cff9-8a10-4cc3-bab0-7fa0e122d7f5");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_At",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "52731f54-d087-473f-8c7a-334baded60ab", null, "Admin", "Admin" },
                    { "c11599c2-b1c0-4717-92ee-b7c9aace767f", null, "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52731f54-d087-473f-8c7a-334baded60ab");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c11599c2-b1c0-4717-92ee-b7c9aace767f");

            migrationBuilder.DropColumn(
                name: "Created_At",
                table: "Products");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01dce6f1-9673-4c1b-960c-4544ec292f7e", null, "User", "User" },
                    { "5127cff9-8a10-4cc3-bab0-7fa0e122d7f5", null, "Admin", "Admin" }
                });
        }
    }
}
