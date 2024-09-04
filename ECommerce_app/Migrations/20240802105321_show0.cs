using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerce_app.Migrations
{
    /// <inheritdoc />
    public partial class show0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "409242a5-3aae-48af-91a2-9bf065afea33");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72b06dc7-f284-45c7-aa8b-d3f6fcfe6c72");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "CartItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "92f11474-c080-4160-9f0b-ebd6d6652e00", null, "User", "User" },
                    { "e826b9db-d680-4d8e-8bbd-82bdd2e56b2d", null, "Admin", "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ApplicationUserId",
                table: "CartItems",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_AspNetUsers_ApplicationUserId",
                table: "CartItems",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_AspNetUsers_ApplicationUserId",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ApplicationUserId",
                table: "CartItems");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "92f11474-c080-4160-9f0b-ebd6d6652e00");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e826b9db-d680-4d8e-8bbd-82bdd2e56b2d");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CartItems");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "409242a5-3aae-48af-91a2-9bf065afea33", null, "User", "User" },
                    { "72b06dc7-f284-45c7-aa8b-d3f6fcfe6c72", null, "Admin", "Admin" }
                });
        }
    }
}
