using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerce_app.Migrations
{
    /// <inheritdoc />
    public partial class data12345 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70fe2eee-cfaf-4ae8-9fd8-163f134c6652");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de83651b-9845-4418-8be6-a193b007f16e");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "userAddressNew");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "77fba3ed-9625-4f55-9ace-e4baba3a4ff8", null, "Admin", null },
                    { "d0741f95-e094-4e24-949b-367672b8d3af", null, "User", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77fba3ed-9625-4f55-9ace-e4baba3a4ff8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0741f95-e094-4e24-949b-367672b8d3af");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "userAddressNew",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "70fe2eee-cfaf-4ae8-9fd8-163f134c6652", null, "Admin", null },
                    { "de83651b-9845-4418-8be6-a193b007f16e", null, "User", null }
                });
        }
    }
}
