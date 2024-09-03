using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { new Guid("654b46f9-dbce-4fe5-8280-8fbe4e95e2d5"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "oncellhsyn@outlook.com", "Hüseyin", "ÖNCEL", new byte[] { 219, 249, 136, 182, 23, 107, 222, 158, 238, 234, 55, 130, 69, 229, 84, 123, 33, 48, 139, 188, 11, 73, 118, 89, 60, 63, 238, 113, 212, 73, 97, 107, 154, 37, 84, 93, 34, 14, 217, 164, 150, 149, 107, 118, 151, 240, 155, 141, 68, 173, 30, 202, 167, 165, 113, 191, 85, 83, 67, 199, 175, 72, 110, 46 }, new byte[] { 174, 0, 12, 86, 22, 61, 192, 210, 16, 128, 59, 17, 228, 188, 251, 226, 44, 67, 233, 223, 129, 165, 166, 101, 84, 27, 3, 142, 215, 246, 100, 193, 202, 179, 105, 105, 79, 217, 191, 120, 253, 165, 51, 181, 82, 179, 110, 100, 65, 37, 46, 222, 53, 78, 142, 155, 218, 18, 147, 2, 146, 85, 40, 146, 126, 201, 136, 71, 16, 92, 95, 99, 5, 188, 13, 113, 74, 108, 122, 164, 212, 43, 209, 21, 127, 177, 144, 188, 255, 116, 192, 232, 74, 214, 3, 214, 206, 118, 81, 175, 49, 173, 14, 217, 124, 95, 74, 131, 194, 26, 120, 68, 212, 118, 83, 194, 2, 209, 54, 137, 67, 150, 27, 107, 26, 71, 119, 158 }, null, 2 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("36c73b20-082e-4aa4-83e9-cea8a8fa4103"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("654b46f9-dbce-4fe5-8280-8fbe4e95e2d5") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("36c73b20-082e-4aa4-83e9-cea8a8fa4103"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("654b46f9-dbce-4fe5-8280-8fbe4e95e2d5"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DeletedDate", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("0b604a72-050f-46fc-9e46-8fe6bb23056f"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "oncellhsyn@outlook.com", "Hüseyin", "ÖNCEL", new byte[] { 22, 32, 130, 8, 59, 44, 186, 90, 78, 144, 55, 20, 123, 192, 50, 24, 68, 118, 79, 73, 36, 150, 48, 201, 22, 184, 30, 16, 17, 58, 70, 135, 0, 88, 174, 192, 209, 199, 118, 239, 208, 223, 107, 14, 45, 182, 229, 111, 124, 207, 164, 252, 105, 33, 218, 107, 90, 145, 152, 115, 204, 202, 88, 222 }, new byte[] { 246, 236, 210, 62, 20, 5, 19, 120, 176, 103, 193, 49, 100, 238, 151, 182, 253, 101, 101, 172, 78, 133, 39, 132, 24, 184, 22, 3, 47, 97, 118, 56, 45, 71, 91, 82, 167, 159, 125, 241, 81, 82, 87, 111, 48, 87, 177, 27, 191, 113, 249, 57, 114, 74, 192, 168, 135, 68, 44, 89, 2, 116, 61, 240, 170, 213, 174, 11, 101, 119, 133, 28, 230, 100, 188, 116, 211, 3, 214, 92, 148, 107, 116, 86, 54, 210, 45, 167, 249, 136, 231, 62, 129, 251, 171, 214, 224, 61, 11, 192, 2, 131, 224, 56, 70, 34, 132, 26, 173, 83, 157, 243, 79, 95, 59, 197, 127, 12, 217, 6, 224, 71, 171, 33, 5, 118, 235, 175 }, null, 2 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("31b244a1-ed69-4994-b2a4-ca5952c52b22"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("0b604a72-050f-46fc-9e46-8fe6bb23056f") });
        }
    }
}
