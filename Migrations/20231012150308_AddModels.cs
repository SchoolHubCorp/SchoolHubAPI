using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolHubApi.Migrations
{
    /// <inheritdoc />
    public partial class AddModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    ClassId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Classname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Plan = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ClassId);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "GradeNames",
                columns: table => new
                {
                    GradeNameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeNames", x => x.GradeNameId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Plan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessCode = table.Column<int>(type: "int", nullable: false),
                    Pesel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "CourseClasses",
                columns: table => new
                {
                    CourseClassId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseClasses", x => x.CourseClassId);
                    table.ForeignKey(
                        name: "FK_CourseClasses_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "ClassId");
                    table.ForeignKey(
                        name: "FK_CourseClasses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                    table.ForeignKey(
                        name: "FK_CourseClasses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Parentchildren",
                columns: table => new
                {
                    ParentChildId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChildUserId = table.Column<int>(type: "int", nullable: false),
                    ParentUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parentchildren", x => x.ParentChildId);
                    table.ForeignKey(
                        name: "FK_Parentchildren_Users_ChildUserId",
                        column: x => x.ChildUserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Parentchildren_Users_ParentUserId",
                        column: x => x.ParentUserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Pupils",
                columns: table => new
                {
                    PupilId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pupils", x => x.PupilId);
                    table.ForeignKey(
                        name: "FK_Pupils_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "ClassId");
                    table.ForeignKey(
                        name: "FK_Pupils_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.UserRoleId);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Homeworks",
                columns: table => new
                {
                    HomeworkId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeworkFile = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CourseClassId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homeworks", x => x.HomeworkId);
                    table.ForeignKey(
                        name: "FK_Homeworks_CourseClasses_CourseClassId",
                        column: x => x.CourseClassId,
                        principalTable: "CourseClasses",
                        principalColumn: "CourseClassId");
                });

            migrationBuilder.CreateTable(
                name: "ClassCoursePupilGrades",
                columns: table => new
                {
                    ClassCoursePupilGradeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grade = table.Column<double>(type: "float", nullable: false),
                    Semestr = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CourseClassId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GradeNameId = table.Column<int>(type: "int", nullable: true),
                    PupilId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassCoursePupilGrades", x => x.ClassCoursePupilGradeId);
                    table.ForeignKey(
                        name: "FK_ClassCoursePupilGrades_CourseClasses_CourseClassId",
                        column: x => x.CourseClassId,
                        principalTable: "CourseClasses",
                        principalColumn: "CourseClassId");
                    table.ForeignKey(
                        name: "FK_ClassCoursePupilGrades_GradeNames_GradeNameId",
                        column: x => x.GradeNameId,
                        principalTable: "GradeNames",
                        principalColumn: "GradeNameId");
                    table.ForeignKey(
                        name: "FK_ClassCoursePupilGrades_Pupils_PupilId",
                        column: x => x.PupilId,
                        principalTable: "Pupils",
                        principalColumn: "PupilId");
                });

            migrationBuilder.CreateTable(
                name: "HomeworkPupils",
                columns: table => new
                {
                    HomeworkPupilId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    File = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PupilId = table.Column<int>(type: "int", nullable: false),
                    ClassCoursePupilGradeId = table.Column<int>(type: "int", nullable: true),
                    HomeworkId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeworkPupils", x => x.HomeworkPupilId);
                    table.ForeignKey(
                        name: "FK_HomeworkPupils_ClassCoursePupilGrades_ClassCoursePupilGradeId",
                        column: x => x.ClassCoursePupilGradeId,
                        principalTable: "ClassCoursePupilGrades",
                        principalColumn: "ClassCoursePupilGradeId");
                    table.ForeignKey(
                        name: "FK_HomeworkPupils_Homeworks_HomeworkId",
                        column: x => x.HomeworkId,
                        principalTable: "Homeworks",
                        principalColumn: "HomeworkId");
                    table.ForeignKey(
                        name: "FK_HomeworkPupils_Pupils_PupilId",
                        column: x => x.PupilId,
                        principalTable: "Pupils",
                        principalColumn: "PupilId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassCoursePupilGrades_CourseClassId",
                table: "ClassCoursePupilGrades",
                column: "CourseClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassCoursePupilGrades_GradeNameId",
                table: "ClassCoursePupilGrades",
                column: "GradeNameId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassCoursePupilGrades_PupilId",
                table: "ClassCoursePupilGrades",
                column: "PupilId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseClasses_ClassId",
                table: "CourseClasses",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseClasses_CourseId",
                table: "CourseClasses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseClasses_UserId",
                table: "CourseClasses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkPupils_ClassCoursePupilGradeId",
                table: "HomeworkPupils",
                column: "ClassCoursePupilGradeId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkPupils_HomeworkId",
                table: "HomeworkPupils",
                column: "HomeworkId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkPupils_PupilId",
                table: "HomeworkPupils",
                column: "PupilId");

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_CourseClassId",
                table: "Homeworks",
                column: "CourseClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Parentchildren_ChildUserId",
                table: "Parentchildren",
                column: "ChildUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Parentchildren_ParentUserId",
                table: "Parentchildren",
                column: "ParentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pupils_ClassId",
                table: "Pupils",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Pupils_UserId",
                table: "Pupils",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeworkPupils");

            migrationBuilder.DropTable(
                name: "Parentchildren");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "ClassCoursePupilGrades");

            migrationBuilder.DropTable(
                name: "Homeworks");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "GradeNames");

            migrationBuilder.DropTable(
                name: "Pupils");

            migrationBuilder.DropTable(
                name: "CourseClasses");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
