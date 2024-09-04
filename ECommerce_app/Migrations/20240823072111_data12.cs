using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerce_app.Migrations
{
    /// <inheritdoc />
    public partial class data12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6717388a-572d-4b45-a788-8830f8cb0eea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a21b9f9-19b2-4a38-88b5-e18703fa4c62");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "userAddressNew",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "userAddressNew",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "e8dc1bba-83f6-4a70-8fec-1eb44a58faf2", null, "Admin", null },
                    { "f31e842a-4dae-4675-aa47-26add07d5e15", null, "User", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8dc1bba-83f6-4a70-8fec-1eb44a58faf2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f31e842a-4dae-4675-aa47-26add07d5e15");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "userAddressNew");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "userAddressNew");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6717388a-572d-4b45-a788-8830f8cb0eea", null, "User", null },
                    { "6a21b9f9-19b2-4a38-88b5-e18703fa4c62", null, "Admin", null }
                });
        }
    }
}
