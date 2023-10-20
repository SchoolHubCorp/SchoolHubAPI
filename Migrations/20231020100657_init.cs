using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolHubApi.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Pesel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Parents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserDataEmail = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parents_Users_UserDataEmail",
                        column: x => x.UserDataEmail,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pupils",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccessCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserDataEmail = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pupils", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pupils_Users_UserDataEmail",
                        column: x => x.UserDataEmail,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResetPasswordCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ResetCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResetPasswordCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResetPasswordCodes_Users_Email",
                        column: x => x.Email,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserDataEmail = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_Users_UserDataEmail",
                        column: x => x.UserDataEmail,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParentPupil",
                columns: table => new
                {
                    ChildrenId = table.Column<int>(type: "int", nullable: false),
                    ParentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentPupil", x => new { x.ChildrenId, x.ParentsId });
                    table.ForeignKey(
                        name: "FK_ParentPupil_Parents_ParentsId",
                        column: x => x.ParentsId,
                        principalTable: "Parents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ParentPupil_Pupils_ChildrenId",
                        column: x => x.ChildrenId,
                        principalTable: "Pupils",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "Pesel", "PhoneNumber", "Role" },
                values: new object[] { "schoolhubpl@gmail.com", "Admin", "", new byte[] { 201, 152, 95, 29, 178, 101, 170, 219, 202, 203, 107, 94, 90, 12, 5, 30, 132, 102, 199, 151, 235, 36, 134, 250, 20, 196, 46, 114, 80, 11, 3, 237, 96, 104, 234, 106, 204, 72, 30, 126, 217, 44, 6, 238, 60, 188, 46, 211, 148, 177, 20, 193, 90, 239, 58, 73, 212, 241, 183, 254, 243, 121, 122, 254 }, new byte[] { 97, 101, 182, 101, 161, 142, 231, 28, 215, 166, 92, 202, 216, 190, 70, 94, 134, 144, 38, 248, 154, 183, 13, 228, 38, 82, 24, 78, 74, 168, 254, 239, 70, 79, 74, 230, 99, 18, 165, 205, 120, 62, 65, 151, 105, 167, 207, 250, 36, 56, 134, 68, 211, 36, 165, 141, 125, 148, 116, 206, 28, 120, 181, 73, 106, 20, 137, 62, 243, 90, 13, 35, 48, 61, 75, 205, 254, 87, 38, 206, 30, 5, 76, 173, 76, 70, 31, 204, 15, 8, 84, 111, 165, 143, 35, 88, 7, 34, 146, 123, 242, 39, 195, 180, 202, 197, 2, 22, 89, 46, 130, 112, 26, 251, 53, 172, 181, 137, 93, 176, 168, 114, 62, 115, 107, 226, 94, 154 }, "", "", "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_ParentPupil_ParentsId",
                table: "ParentPupil",
                column: "ParentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_UserDataEmail",
                table: "Parents",
                column: "UserDataEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Pupils_AccessCode",
                table: "Pupils",
                column: "AccessCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pupils_UserDataEmail",
                table: "Pupils",
                column: "UserDataEmail");

            migrationBuilder.CreateIndex(
                name: "IX_ResetPasswordCodes_Email",
                table: "ResetPasswordCodes",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_UserDataEmail",
                table: "Teachers",
                column: "UserDataEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParentPupil");

            migrationBuilder.DropTable(
                name: "ResetPasswordCodes");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Parents");

            migrationBuilder.DropTable(
                name: "Pupils");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
