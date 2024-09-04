using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerce_app.Migrations
{
    /// <inheritdoc />
    public partial class show12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82348e86-1b70-47a0-99f9-522121da92d0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c3f1d9b2-7f9a-4c20-bbcb-7fdfa7431dc5");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "226dab75-d456-4543-a093-23d291cfc9b1", null, "User", "User" },
                    { "b38fe6ea-0c98-49a2-acca-63575ec79fb4", null, "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "226dab75-d456-4543-a093-23d291cfc9b1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b38fe6ea-0c98-49a2-acca-63575ec79fb4");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "82348e86-1b70-47a0-99f9-522121da92d0", null, "Admin", "Admin" },
                    { "c3f1d9b2-7f9a-4c20-bbcb-7fdfa7431dc5", null, "User", "User" }
                });
        }
    }
}
