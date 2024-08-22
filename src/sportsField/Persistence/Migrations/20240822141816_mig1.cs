using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("894bd71a-af20-41f8-94aa-34119d9c4e3f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("96c7fe8d-156b-4f0a-a72d-4960dc7843b1"));

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Courts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Courts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "CourtReservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DeletedDate", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("f3c1c35b-f2c0-4681-bb91-203e3f330d85"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "oncellhsyn@outlook.com", "Hüseyin", "ÖNCEL", new byte[] { 148, 246, 87, 102, 182, 218, 212, 173, 19, 56, 191, 129, 87, 139, 166, 3, 117, 13, 240, 248, 225, 128, 187, 187, 219, 48, 177, 31, 211, 65, 20, 235, 167, 137, 63, 220, 177, 6, 161, 82, 254, 164, 186, 213, 93, 93, 137, 217, 234, 99, 20, 66, 249, 181, 250, 111, 196, 228, 51, 137, 84, 252, 32, 105 }, new byte[] { 233, 65, 28, 74, 214, 82, 101, 31, 201, 233, 20, 25, 240, 54, 3, 23, 195, 193, 246, 203, 189, 174, 255, 1, 15, 60, 79, 120, 50, 206, 209, 158, 196, 219, 205, 11, 86, 74, 246, 247, 3, 6, 217, 58, 31, 43, 19, 78, 238, 122, 41, 220, 214, 46, 100, 16, 127, 218, 95, 229, 124, 8, 20, 12, 163, 25, 62, 205, 112, 140, 246, 83, 168, 19, 61, 250, 27, 34, 83, 100, 146, 233, 6, 97, 142, 228, 70, 30, 15, 44, 106, 158, 101, 14, 53, 87, 90, 105, 125, 243, 98, 81, 13, 18, 81, 20, 99, 240, 66, 100, 104, 92, 33, 218, 198, 14, 156, 45, 20, 164, 184, 111, 153, 71, 225, 138, 230, 44 }, null, 2 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("4fae71d2-6ed6-4fee-a347-33b4eb8f73a7"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("f3c1c35b-f2c0-4681-bb91-203e3f330d85") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserOperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("4fae71d2-6ed6-4fee-a347-33b4eb8f73a7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f3c1c35b-f2c0-4681-bb91-203e3f330d85"));

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Courts");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Courts");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "CourtReservations");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DeletedDate", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "UpdatedDate", "UserState" },
                values: new object[] { new Guid("96c7fe8d-156b-4f0a-a72d-4960dc7843b1"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "oncellhsyn@outlook.com", "Hüseyin", "ÖNCEL", new byte[] { 35, 185, 39, 250, 158, 125, 176, 2, 165, 185, 252, 117, 244, 199, 240, 33, 125, 158, 128, 49, 218, 12, 109, 87, 226, 67, 43, 220, 14, 211, 154, 111, 199, 200, 99, 188, 18, 209, 28, 120, 149, 140, 223, 45, 74, 66, 246, 26, 93, 200, 111, 188, 198, 198, 31, 52, 0, 204, 76, 3, 167, 168, 212, 194 }, new byte[] { 28, 54, 42, 169, 169, 13, 107, 20, 102, 20, 108, 99, 174, 233, 108, 38, 109, 30, 28, 56, 103, 149, 224, 108, 6, 228, 130, 163, 184, 163, 177, 106, 89, 111, 223, 198, 150, 99, 194, 181, 210, 102, 227, 248, 118, 154, 157, 138, 199, 232, 117, 21, 101, 176, 218, 171, 9, 161, 112, 202, 154, 119, 144, 22, 95, 73, 125, 78, 211, 17, 88, 76, 48, 250, 197, 15, 4, 83, 155, 78, 232, 194, 36, 20, 245, 137, 34, 91, 241, 69, 180, 252, 87, 240, 100, 100, 114, 214, 158, 171, 182, 36, 103, 63, 225, 155, 120, 113, 254, 116, 51, 237, 27, 102, 207, 248, 153, 160, 153, 89, 206, 108, 53, 55, 1, 236, 59, 203 }, null, 2 });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "OperationClaimId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("894bd71a-af20-41f8-94aa-34119d9c4e3f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("96c7fe8d-156b-4f0a-a72d-4960dc7843b1") });
        }
    }
}
