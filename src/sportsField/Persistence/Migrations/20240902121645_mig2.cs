using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("4fae71d2-6ed6-4fee-a347-33b4eb8f73a7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f3c1c35b-f2c0-4681-bb91-203e3f330d85"));

            migrationBuilder.CreateTable(
                name: "Retentions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Command = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retentions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Retentions_Users_UserId",
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
                    { 42, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Retentions.Admin", null },
                    { 43, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Retentions.Read", null },
                    { 44, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Retentions.Write", null },
                    { 45, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Retentions.Create", null },
                    { 46, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Retentions.Update", null },
                    { 47, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Retentions.Delete", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DeletedDate", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("0b604a72-050f-46fc-9e46-8fe6bb23056f"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "oncellhsyn@outlook.com", "Hüseyin", "ÖNCEL", new byte[] { 22, 32, 130, 8, 59, 44, 186, 90, 78, 144, 55, 20, 123, 192, 50, 24, 68, 118, 79, 73, 36, 150, 48, 201, 22, 184, 30, 16, 17, 58, 70, 135, 0, 88, 174, 192, 209, 199, 118, 239, 208, 223, 107, 14, 45, 182, 229, 111, 124, 207, 164, 252, 105, 33, 218, 107, 90, 145, 152, 115, 204, 202, 88, 222 }, new byte[] { 246, 236, 210, 62, 20, 5, 19, 120, 176, 103, 193, 49, 100, 238, 151, 182, 253, 101, 101, 172, 78, 133, 39, 132, 24, 184, 22, 3, 47, 97, 118, 56, 45, 71, 91, 82, 167, 159, 125, 241, 81, 82, 87, 111, 48, 87, 177, 27, 191, 113, 249, 57, 114, 74, 192, 168, 135, 68, 44, 89, 2, 116, 61, 240, 170, 213, 174, 11, 101, 119, 133, 28, 230, 100, 188, 116, 211, 3, 214, 92, 148, 107, 116, 86, 54, 210, 45, 167, 249, 136, 231, 62, 129, 251, 171, 214, 224, 61, 11, 192, 2, 131, 224, 56, 70, 34, 132, 26, 173, 83, 157, 243, 79, 95, 59, 197, 127, 12, 217, 6, 224, 71, 171, 33, 5, 118, 235, 175 }, null, 2 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("31b244a1-ed69-4994-b2a4-ca5952c52b22"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("0b604a72-050f-46fc-9e46-8fe6bb23056f") });

            migrationBuilder.CreateIndex(
                name: "IX_Retentions_UserId",
                table: "Retentions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UK_Retention_Name",
                table: "Retentions",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Retentions");

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("31b244a1-ed69-4994-b2a4-ca5952c52b22"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0b604a72-050f-46fc-9e46-8fe6bb23056f"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DeletedDate", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("f3c1c35b-f2c0-4681-bb91-203e3f330d85"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "oncellhsyn@outlook.com", "Hüseyin", "ÖNCEL", new byte[] { 148, 246, 87, 102, 182, 218, 212, 173, 19, 56, 191, 129, 87, 139, 166, 3, 117, 13, 240, 248, 225, 128, 187, 187, 219, 48, 177, 31, 211, 65, 20, 235, 167, 137, 63, 220, 177, 6, 161, 82, 254, 164, 186, 213, 93, 93, 137, 217, 234, 99, 20, 66, 249, 181, 250, 111, 196, 228, 51, 137, 84, 252, 32, 105 }, new byte[] { 233, 65, 28, 74, 214, 82, 101, 31, 201, 233, 20, 25, 240, 54, 3, 23, 195, 193, 246, 203, 189, 174, 255, 1, 15, 60, 79, 120, 50, 206, 209, 158, 196, 219, 205, 11, 86, 74, 246, 247, 3, 6, 217, 58, 31, 43, 19, 78, 238, 122, 41, 220, 214, 46, 100, 16, 127, 218, 95, 229, 124, 8, 20, 12, 163, 25, 62, 205, 112, 140, 246, 83, 168, 19, 61, 250, 27, 34, 83, 100, 146, 233, 6, 97, 142, 228, 70, 30, 15, 44, 106, 158, 101, 14, 53, 87, 90, 105, 125, 243, 98, 81, 13, 18, 81, 20, 99, 240, 66, 100, 104, 92, 33, 218, 198, 14, 156, 45, 20, 164, 184, 111, 153, 71, 225, 138, 230, 44 }, null, 2 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("4fae71d2-6ed6-4fee-a347-33b4eb8f73a7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("f3c1c35b-f2c0-4681-bb91-203e3f330d85") });
        }
    }
}
