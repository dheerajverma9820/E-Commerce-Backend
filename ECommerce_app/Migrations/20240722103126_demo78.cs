using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerce_app.Migrations
{
    /// <inheritdoc />
    public partial class demo78 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52f12e69-828a-415f-b47f-fe753029f9f6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a23e041c-4469-4d10-9311-6e6c615018f3");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Inventories");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1ffd1113-5a63-48a4-bcce-8b3df2d4b5bd", null, "User", "User" },
                    { "c403fb3c-bda4-407e-a813-ec9bcf892ca2", null, "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1ffd1113-5a63-48a4-bcce-8b3df2d4b5bd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c403fb3c-bda4-407e-a813-ec9bcf892ca2");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Inventories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "52f12e69-828a-415f-b47f-fe753029f9f6", null, "User", "User" },
                    { "a23e041c-4469-4d10-9311-6e6c615018f3", null, "Admin", "Admin" }
                });
        }
    }
}
