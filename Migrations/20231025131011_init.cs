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
                values: new object[] { "schoolhubpl@gmail.com", "Admin", "", new byte[] { 50, 122, 141, 108, 7, 141, 162, 122, 140, 212, 111, 39, 41, 165, 16, 187, 40, 196, 124, 120, 69, 181, 159, 88, 226, 134, 23, 146, 195, 71, 225, 162, 230, 69, 159, 184, 119, 210, 243, 112, 129, 21, 139, 96, 205, 120, 174, 159, 104, 204, 253, 173, 51, 52, 157, 64, 173, 113, 86, 9, 222, 121, 215, 128 }, new byte[] { 4, 25, 201, 126, 163, 215, 123, 89, 252, 25, 138, 245, 107, 70, 196, 129, 214, 108, 176, 35, 60, 132, 250, 24, 111, 145, 230, 73, 115, 203, 27, 197, 51, 253, 60, 59, 46, 102, 96, 181, 214, 245, 53, 113, 16, 113, 56, 2, 26, 220, 56, 9, 147, 96, 49, 121, 98, 82, 117, 34, 55, 184, 143, 51, 187, 195, 181, 57, 235, 71, 185, 194, 129, 181, 96, 213, 69, 6, 96, 242, 135, 17, 197, 172, 203, 60, 111, 253, 244, 183, 86, 249, 202, 30, 159, 191, 29, 162, 19, 112, 59, 177, 53, 109, 104, 54, 203, 54, 123, 191, 145, 34, 104, 8, 179, 132, 34, 73, 37, 235, 66, 2, 33, 222, 151, 121, 55, 23 }, "", "", "Admin" });

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
