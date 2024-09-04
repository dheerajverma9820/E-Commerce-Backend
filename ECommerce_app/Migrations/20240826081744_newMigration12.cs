using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerce_app.Migrations
{
    /// <inheritdoc />
    public partial class newMigration12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1474edc2-69c2-47e7-a9f5-587564cf65c9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "27cb8a84-ecbd-4613-84bc-038c3f38e401");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Brands");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "897b68c5-3f9f-4bc0-9384-e714591bf1c0", null, "User", null },
                    { "f446efe7-22fc-483d-8bc8-d6bf8791f3c0", null, "Admin", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "897b68c5-3f9f-4bc0-9384-e714591bf1c0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f446efe7-22fc-483d-8bc8-d6bf8791f3c0");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1474edc2-69c2-47e7-a9f5-587564cf65c9", null, "User", null },
                    { "27cb8a84-ecbd-4613-84bc-038c3f38e401", null, "Admin", null }
                });
        }
    }
}
