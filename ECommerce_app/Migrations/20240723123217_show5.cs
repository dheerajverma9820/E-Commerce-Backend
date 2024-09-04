using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerce_app.Migrations
{
    /// <inheritdoc />
    public partial class show5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52731f54-d087-473f-8c7a-334baded60ab");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c11599c2-b1c0-4717-92ee-b7c9aace767f");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProductCategorys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d2575121-822b-4ed6-af35-0524848f3e53", null, "User", "User" },
                    { "f8ac42fe-7cac-4906-8f36-66938e677733", null, "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d2575121-822b-4ed6-af35-0524848f3e53");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8ac42fe-7cac-4906-8f36-66938e677733");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProductCategorys");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "52731f54-d087-473f-8c7a-334baded60ab", null, "Admin", "Admin" },
                    { "c11599c2-b1c0-4717-92ee-b7c9aace767f", null, "User", "User" }
                });
        }
    }
}
