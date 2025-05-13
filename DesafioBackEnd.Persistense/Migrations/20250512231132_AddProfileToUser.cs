using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockManager.Persistense.Migrations
{
    /// <inheritdoc />
    public partial class AddProfileToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProfileId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Password", "ProfileId" },
                values: new object[] { "$2a$10$BxAGNBEuWmZS4NHWmHICmuGlPwSJZRwIkNYL24qFRJgXN.VQE44DG", 1L });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfileId",
                table: "Users",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_profile_ProfileId",
                table: "Users",
                column: "ProfileId",
                principalTable: "profile",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_profile_ProfileId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ProfileId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Password",
                value: "$2a$10$jzvzHyTPOi7yiPlXGAFnaedyHXqecPfii/tBqLPJIv1/l506IBBvS");
        }
    }
}
