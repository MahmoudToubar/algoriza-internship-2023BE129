using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VezeetaManagement.Migrations
{
    /// <inheritdoc />
    public partial class sec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a02df147-569e-4cad-b629-a44c8c11091c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9d3ee1b3-4a7b-439d-8893-c5117f26a893", "AQAAAAIAAYagAAAAEOaNZif/jAWZVX9uxmqG+2Jq6ec5JV5m1582QQ7z9Yeg4PtN+clHjJEW+W5hnUJ7Yw==", "cd0f9436-01ee-4ca2-abb2-d27ef5a209f8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a02df147-569e-4cad-b629-a44c8c11091c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6d4fd5fa-769f-445b-9b1c-5ef0a146f1a8", "AQAAAAIAAYagAAAAEIQ6uVXowXFHuTkJsCf6ADIKjzCG6AuK4fpAi0uAeBtuvx3cNkNxAugZoEBU0tNrPw==", "5ec9a71d-c57a-4e7e-8d51-39d5b32c5566" });
        }
    }
}
