﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModelNumber = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recommendation",
                columns: table => new
                {
                    RecommendedCarsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommendation", x => new { x.RecommendedCarsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_Recommendation_Cars_RecommendedCarsId",
                        column: x => x.RecommendedCarsId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recommendation_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "IsActive", "IsAdmin", "LastName", "PasswordHash", "PasswordSalt", "Username" },
                values: new object[] { 1, "Samir", false, true, "Ahmed", new byte[] { 225, 60, 105, 20, 80, 246, 45, 131, 152, 155, 135, 43, 85, 182, 4, 41, 21, 224, 148, 244, 24, 186, 113, 109, 184, 69, 182, 123, 65, 169, 164, 72, 7, 230, 138, 155, 5, 76, 224, 139, 172, 245, 70, 151, 128, 72, 9, 234, 57, 228, 50, 98, 229, 76, 249, 114, 181, 173, 212, 10, 85, 136, 8, 235 }, new byte[] { 109, 148, 57, 32, 175, 251, 120, 248, 34, 155, 162, 157, 227, 184, 59, 114, 65, 251, 229, 88, 163, 228, 38, 228, 177, 231, 188, 32, 60, 48, 78, 96, 10, 203, 161, 4, 203, 189, 237, 18, 191, 111, 216, 52, 141, 169, 179, 146, 234, 217, 217, 49, 183, 182, 26, 22, 92, 91, 230, 110, 69, 139, 14, 143, 238, 218, 250, 232, 230, 213, 105, 203, 26, 13, 122, 123, 234, 234, 244, 240, 4, 252, 156, 101, 74, 148, 66, 42, 192, 238, 98, 239, 118, 61, 89, 29, 42, 177, 78, 167, 177, 220, 37, 137, 89, 182, 74, 51, 45, 63, 16, 245, 0, 32, 115, 248, 102, 197, 152, 220, 212, 22, 38, 102, 228, 5, 179, 168 }, "captain" });

            migrationBuilder.CreateIndex(
                name: "IX_Recommendation_UsersId",
                table: "Recommendation",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recommendation");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
