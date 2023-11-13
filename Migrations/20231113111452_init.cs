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
                    TeacherPlan = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    TeacherPlanContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassroomId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "Pesel", "PhoneNumber", "Role" },
                values: new object[] { "schoolhubpl@gmail.com", "Admin", "", new byte[] { 247, 226, 73, 47, 63, 22, 219, 125, 173, 88, 149, 111, 120, 120, 161, 17, 146, 141, 129, 209, 112, 229, 200, 11, 92, 79, 230, 247, 132, 122, 157, 43, 192, 113, 175, 127, 96, 208, 77, 76, 57, 109, 47, 57, 110, 100, 123, 15, 172, 107, 144, 55, 140, 55, 183, 171, 6, 92, 161, 176, 88, 85, 153, 17 }, new byte[] { 246, 250, 37, 59, 31, 89, 205, 92, 32, 145, 209, 66, 97, 245, 28, 191, 232, 200, 59, 187, 59, 105, 247, 167, 43, 192, 246, 46, 57, 239, 229, 200, 79, 211, 104, 72, 126, 163, 54, 239, 124, 52, 51, 74, 7, 91, 239, 46, 70, 79, 31, 128, 210, 198, 31, 65, 109, 57, 12, 55, 8, 15, 124, 244, 206, 2, 121, 155, 156, 142, 90, 175, 37, 193, 187, 23, 68, 136, 73, 52, 109, 105, 246, 150, 131, 164, 159, 135, 19, 104, 188, 63, 210, 164, 91, 225, 85, 231, 119, 1, 16, 30, 245, 40, 138, 112, 181, 75, 237, 204, 242, 82, 9, 228, 205, 177, 144, 28, 111, 145, 17, 210, 61, 64, 223, 254, 144, 240 }, "", "", "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_ClassAccessCode",
                table: "Classrooms",
                column: "ClassAccessCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ClassroomId",
                table: "Courses",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentPupil_ParentsId",
                table: "ParentPupil",
                column: "ParentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_UserDataEmail",
                table: "Parents",
                column: "UserDataEmail",
                unique: true);

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
                column: "UserDataEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResetPasswordCodes_Email",
                table: "ResetPasswordCodes",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_UserDataEmail",
                table: "Teachers",
                column: "UserDataEmail",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

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
