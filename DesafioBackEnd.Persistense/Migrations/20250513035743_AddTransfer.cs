using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockManager.Persistense.Migrations
{
    /// <inheritdoc />
    public partial class AddTransfer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Password",
                value: "$2a$10$wCs/43hGinj7YPlNz6ws5efQyqE2TGM8AqkY3YQ0ftoogiZudjtVy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Password",
                value: "$2a$10$BxAGNBEuWmZS4NHWmHICmuGlPwSJZRwIkNYL24qFRJgXN.VQE44DG");
        }
    }
}
