using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymManagementSystem.data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StaffAndMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Shifts = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExperienceYears = table.Column<int>(type: "int", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SessionsAvailable = table.Column<int>(type: "int", nullable: true),
                    IsPrivate = table.Column<bool>(type: "Bit", nullable: true),
                    CoachId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffAndMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffAndMembers_StaffAndMembers_CoachId",
                        column: x => x.CoachId,
                        principalTable: "StaffAndMembers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JoinDate = table.Column<DateOnly>(type: "Date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "Date", nullable: false),
                    Plans = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_StaffAndMembers_MemberId",
                        column: x => x.MemberId,
                        principalTable: "StaffAndMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "StaffAndMembers",
                columns: new[] { "Id", "Age", "Email", "FullName", "Gender", "Password", "Phone", "Role", "Shifts" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), 32, "admin@gym.com", "Sayed Ahmed", "Male", "$2a$11$WXHKRbxbTYUP8.EDYMqs8ej/E1/3oEurvQMl2O2tCbXTd/.PYmn4G", "01099999999", "Admin", "Morning" },
                    { new Guid("10000000-0000-0000-0000-000000000002"), 26, "support@gym.com", "Nour El-Sayed", "Female", "$2a$11$BGh.QBPmTb2/ag0t10C2Z.e86xsq5hBZQVOcXHb6vzAoXUMNtlea6", "01188888888", "Admin", "Evening" }
                });

            migrationBuilder.InsertData(
                table: "StaffAndMembers",
                columns: new[] { "Id", "Age", "Email", "ExperienceYears", "FullName", "Gender", "Password", "Phone", "Role" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), 35, "ahmed.ali@gym.com", 6, "Ahmed Ali", "Male", "$2a$11$MSGydp8Ry8O2TB.ucwg3I.XpUvqjt1J8fjPkXJrOmxeW7uItZHKxe", "01000000000", "Coach" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 30, "sara.mohamed@gym.com", 3, "Sara Mohamed", "Female", "$2a$11$MA/THxwDyYVgaDrqqhUAzuhoeNlVyaiEE0mUuWW71Y1ZLuRR7Ltvq", "01111111111", "Coach" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), 40, "omar.hassan@gym.com", 7, "Omar Hassan", "Male", "$2a$11$O5iOsLS9ZaSE9nwSC8tNz.V1qaQRn3hzbyuKFwW/ywgTBr5M8h5Sq", "01222222222", "Coach" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StaffAndMembers_CoachId",
                table: "StaffAndMembers",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_MemberId",
                table: "Subscriptions",
                column: "MemberId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "StaffAndMembers");
        }
    }
}
