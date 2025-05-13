using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockManager.Persistense.Migrations
{
    /// <inheritdoc />
    public partial class WalletPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PermissionGroups",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Name", "Status", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 2L, null, null, "Wallet", "A", null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Password",
                value: "$2a$10$irYAuE5BMszN43ux4uQ7I.chHkg6zTV4mpiGEVRINf6SiP7e/0pbG");

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "Name", "PermissionGroupId", "Status", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 6L, null, null, "Add balance", "ADD_BALANCE", 2L, "A", null, null },
                    { 7L, null, null, "Remove balance", "REMOVE_BALANCE", 2L, "A", null, null },
                    { 8L, null, null, "Get balance", "GET_BALANCE", 2L, "A", null, null },
                    { 9L, null, null, "Create transference", "CREATE_TRANSFERENCE", 2L, "A", null, null },
                    { 10L, null, null, "Get transference", "GET_TRANSFERENCE", 2L, "A", null, null },
                    { 11L, null, null, "List transference", "LIST_TRANSFERENCE", 2L, "A", null, null }
                });

            migrationBuilder.InsertData(
                table: "ProfilePermissions",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "PermissionId", "ProfileEntityId", "Status", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 6L, null, null, 6L, 1L, "A", null, null },
                    { 7L, null, null, 7L, 1L, "A", null, null },
                    { 8L, null, null, 8L, 1L, "A", null, null },
                    { 9L, null, null, 9L, 1L, "A", null, null },
                    { 10L, null, null, 10L, 1L, "A", null, null },
                    { 11L, null, null, 11L, 1L, "A", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProfilePermissions",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "ProfilePermissions",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "ProfilePermissions",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "ProfilePermissions",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "ProfilePermissions",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "ProfilePermissions",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "PermissionGroups",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Password",
                value: "$2a$10$wCs/43hGinj7YPlNz6ws5efQyqE2TGM8AqkY3YQ0ftoogiZudjtVy");
        }
    }
}
