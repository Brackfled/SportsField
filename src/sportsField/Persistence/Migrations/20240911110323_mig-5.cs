using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("fbe9b680-ef8a-4e73-aea2-98a873975a34"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9aea2950-550a-4af4-842a-7b930ced000a"));

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 48, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "SFFile.Admin", null },
                    { 49, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "SFFile.Create", null },
                    { 50, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "SFFile.Delete", null },
                    { 51, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "CourtReservations.Rent", null },
                    { 52, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "CourtReservations.Cancel", null },
                    { 53, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "CeoItems.Read", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DeletedDate", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("1ae6bafa-ffd4-409e-a628-9624c54072ad"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "oncellhsyn@outlook.com", "Hüseyin", "ÖNCEL", new byte[] { 200, 7, 224, 49, 227, 117, 48, 218, 58, 210, 92, 148, 25, 37, 155, 163, 26, 120, 249, 46, 176, 106, 36, 115, 172, 231, 35, 76, 231, 150, 247, 200, 175, 131, 9, 30, 146, 212, 132, 99, 251, 218, 147, 227, 47, 13, 30, 194, 200, 62, 118, 145, 141, 133, 153, 135, 173, 109, 236, 45, 22, 222, 126, 89 }, new byte[] { 215, 232, 72, 214, 132, 157, 174, 20, 147, 71, 101, 43, 85, 150, 114, 142, 94, 35, 19, 228, 17, 163, 175, 173, 202, 170, 188, 129, 46, 183, 220, 110, 98, 168, 190, 220, 54, 55, 162, 205, 51, 7, 40, 229, 24, 17, 24, 69, 128, 13, 111, 77, 202, 73, 244, 116, 212, 246, 58, 203, 231, 94, 10, 13, 125, 141, 185, 193, 85, 12, 83, 199, 47, 119, 187, 251, 139, 162, 114, 138, 162, 97, 141, 81, 233, 73, 27, 3, 30, 67, 27, 24, 156, 189, 245, 248, 226, 172, 50, 167, 132, 247, 114, 191, 161, 91, 165, 17, 61, 240, 192, 102, 255, 239, 174, 57, 2, 78, 12, 237, 185, 114, 117, 229, 86, 214, 201, 40 }, null, 2 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("2249581b-db5a-4f27-a0e7-d533537f3e79"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("1ae6bafa-ffd4-409e-a628-9624c54072ad") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("2249581b-db5a-4f27-a0e7-d533537f3e79"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1ae6bafa-ffd4-409e-a628-9624c54072ad"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DeletedDate", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("9aea2950-550a-4af4-842a-7b930ced000a"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "oncellhsyn@outlook.com", "Hüseyin", "ÖNCEL", new byte[] { 239, 174, 199, 145, 145, 130, 13, 194, 70, 177, 208, 178, 65, 3, 182, 161, 237, 173, 238, 112, 188, 212, 171, 127, 139, 210, 121, 41, 134, 89, 96, 244, 193, 192, 120, 128, 235, 78, 250, 136, 246, 52, 213, 40, 254, 173, 10, 212, 114, 80, 159, 78, 211, 60, 102, 202, 203, 41, 186, 222, 101, 180, 28, 35 }, new byte[] { 37, 88, 45, 231, 241, 149, 1, 123, 204, 123, 137, 190, 227, 197, 202, 49, 142, 25, 62, 108, 117, 49, 107, 220, 107, 74, 139, 163, 250, 48, 21, 150, 82, 114, 150, 237, 121, 70, 113, 130, 7, 96, 87, 202, 61, 29, 64, 214, 128, 232, 1, 41, 218, 202, 160, 202, 230, 40, 84, 241, 2, 225, 193, 33, 32, 50, 98, 199, 204, 49, 142, 245, 40, 78, 245, 7, 19, 32, 62, 193, 36, 47, 48, 131, 39, 208, 87, 40, 135, 129, 36, 51, 33, 241, 3, 12, 200, 222, 114, 110, 80, 242, 69, 136, 126, 161, 239, 32, 136, 244, 202, 204, 194, 107, 20, 90, 92, 185, 231, 211, 162, 1, 9, 103, 210, 211, 93, 61 }, null, 2 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("fbe9b680-ef8a-4e73-aea2-98a873975a34"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("9aea2950-550a-4af4-842a-7b930ced000a") });
        }
    }
}
