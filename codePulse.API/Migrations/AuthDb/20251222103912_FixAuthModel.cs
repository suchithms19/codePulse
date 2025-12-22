using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace codePulse.API.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class FixAuthModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c4b2f1e8-5f3a-4d2e-9c3b-1e2f3a4b5c6d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d5b938a1-babf-40f2-9b04-4d74d6431194", "AQAAAAIAAYagAAAAEFcJm2OuTfYKkP3XMkb7gr6UjgZWn4h87xfPDaOMUZs8IIlYw2QIjkh8vVzyMnrFpQ==", "7d3dcde5-e9ee-4bf6-994e-914d1ff33b4a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c4b2f1e8-5f3a-4d2e-9c3b-1e2f3a4b5c6d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "36f56c4a-c569-4c3e-8062-14ab0d2c3853", "AQAAAAIAAYagAAAAEFKZbiCORXLx05XjGTEbF9LU0vwiz2fiWG4M/flch6hm+btLWu9uiByweiI5qAYQKg==", "3c748b0d-45a2-4c8b-9a23-0d67fdcebf44" });
        }
    }
}
