using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("36c73b20-082e-4aa4-83e9-cea8a8fa4103"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("654b46f9-dbce-4fe5-8280-8fbe4e95e2d5"));

            migrationBuilder.CreateTable(
                name: "Suspends",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SuspensionPeriod = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suspends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suspends_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 54, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Suspends.Admin", null },
                    { 55, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Suspends.Read", null },
                    { 56, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Suspends.Write", null },
                    { 57, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Suspends.Create", null },
                    { 58, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Suspends.Update", null },
                    { 59, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Suspends.Delete", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DeletedDate", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("9aea2950-550a-4af4-842a-7b930ced000a"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "oncellhsyn@outlook.com", "Hüseyin", "ÖNCEL", new byte[] { 239, 174, 199, 145, 145, 130, 13, 194, 70, 177, 208, 178, 65, 3, 182, 161, 237, 173, 238, 112, 188, 212, 171, 127, 139, 210, 121, 41, 134, 89, 96, 244, 193, 192, 120, 128, 235, 78, 250, 136, 246, 52, 213, 40, 254, 173, 10, 212, 114, 80, 159, 78, 211, 60, 102, 202, 203, 41, 186, 222, 101, 180, 28, 35 }, new byte[] { 37, 88, 45, 231, 241, 149, 1, 123, 204, 123, 137, 190, 227, 197, 202, 49, 142, 25, 62, 108, 117, 49, 107, 220, 107, 74, 139, 163, 250, 48, 21, 150, 82, 114, 150, 237, 121, 70, 113, 130, 7, 96, 87, 202, 61, 29, 64, 214, 128, 232, 1, 41, 218, 202, 160, 202, 230, 40, 84, 241, 2, 225, 193, 33, 32, 50, 98, 199, 204, 49, 142, 245, 40, 78, 245, 7, 19, 32, 62, 193, 36, 47, 48, 131, 39, 208, 87, 40, 135, 129, 36, 51, 33, 241, 3, 12, 200, 222, 114, 110, 80, 242, 69, 136, 126, 161, 239, 32, 136, 244, 202, 204, 194, 107, 20, 90, 92, 185, 231, 211, 162, 1, 9, 103, 210, 211, 93, 61 }, null, 2 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("fbe9b680-ef8a-4e73-aea2-98a873975a34"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("9aea2950-550a-4af4-842a-7b930ced000a") });

            migrationBuilder.CreateIndex(
                name: "IX_Suspends_UserId",
                table: "Suspends",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Suspends");

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("fbe9b680-ef8a-4e73-aea2-98a873975a34"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9aea2950-550a-4af4-842a-7b930ced000a"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DeletedDate", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("654b46f9-dbce-4fe5-8280-8fbe4e95e2d5"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "oncellhsyn@outlook.com", "Hüseyin", "ÖNCEL", new byte[] { 219, 249, 136, 182, 23, 107, 222, 158, 238, 234, 55, 130, 69, 229, 84, 123, 33, 48, 139, 188, 11, 73, 118, 89, 60, 63, 238, 113, 212, 73, 97, 107, 154, 37, 84, 93, 34, 14, 217, 164, 150, 149, 107, 118, 151, 240, 155, 141, 68, 173, 30, 202, 167, 165, 113, 191, 85, 83, 67, 199, 175, 72, 110, 46 }, new byte[] { 174, 0, 12, 86, 22, 61, 192, 210, 16, 128, 59, 17, 228, 188, 251, 226, 44, 67, 233, 223, 129, 165, 166, 101, 84, 27, 3, 142, 215, 246, 100, 193, 202, 179, 105, 105, 79, 217, 191, 120, 253, 165, 51, 181, 82, 179, 110, 100, 65, 37, 46, 222, 53, 78, 142, 155, 218, 18, 147, 2, 146, 85, 40, 146, 126, 201, 136, 71, 16, 92, 95, 99, 5, 188, 13, 113, 74, 108, 122, 164, 212, 43, 209, 21, 127, 177, 144, 188, 255, 116, 192, 232, 74, 214, 3, 214, 206, 118, 81, 175, 49, 173, 14, 217, 124, 95, 74, 131, 194, 26, 120, 68, 212, 118, 83, 194, 2, 209, 54, 137, 67, 150, 27, 107, 26, 71, 119, 158 }, null, 2 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("36c73b20-082e-4aa4-83e9-cea8a8fa4103"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("654b46f9-dbce-4fe5-8280-8fbe4e95e2d5") });
        }
    }
}
