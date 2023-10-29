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
                name: "Classrooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Plan = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PlanContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassAccessCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classrooms", x => x.Id);
                });

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
                    ClassroomId = table.Column<int>(type: "int", nullable: false),
                    UserDataEmail = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pupils", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pupils_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                values: new object[] { "schoolhubpl@gmail.com", "Admin", "", new byte[] { 155, 147, 215, 68, 241, 152, 37, 122, 241, 217, 172, 232, 95, 171, 51, 239, 214, 234, 204, 151, 55, 163, 138, 228, 232, 46, 175, 249, 62, 195, 235, 175, 118, 137, 131, 59, 18, 20, 78, 71, 107, 29, 97, 247, 108, 89, 159, 153, 15, 194, 227, 221, 195, 188, 181, 133, 183, 9, 2, 201, 227, 226, 145, 51 }, new byte[] { 38, 177, 218, 68, 170, 108, 141, 95, 166, 118, 249, 163, 119, 217, 2, 154, 213, 131, 188, 238, 84, 150, 117, 123, 74, 68, 102, 191, 207, 93, 165, 34, 206, 108, 129, 245, 139, 80, 173, 37, 26, 122, 180, 92, 108, 255, 183, 193, 160, 199, 156, 29, 247, 35, 44, 132, 57, 77, 180, 199, 30, 75, 162, 130, 215, 75, 204, 119, 40, 39, 140, 196, 61, 91, 90, 87, 229, 217, 195, 142, 13, 230, 35, 200, 41, 223, 163, 51, 135, 239, 107, 193, 93, 147, 52, 197, 226, 47, 17, 242, 120, 50, 22, 38, 198, 217, 190, 17, 128, 239, 204, 226, 119, 153, 217, 220, 226, 142, 225, 142, 168, 167, 54, 91, 36, 8, 99, 87 }, "", "", "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_ClassAccessCode",
                table: "Classrooms",
                column: "ClassAccessCode",
                unique: true);

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
                name: "IX_Pupils_ClassroomId",
                table: "Pupils",
                column: "ClassroomId");

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
                name: "Classrooms");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
