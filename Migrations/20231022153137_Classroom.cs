using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolHubApi.Migrations
{
    /// <inheritdoc />
    public partial class Classroom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassroomId",
                table: "Pupils",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Classrooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Plan = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ClassAccessCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classrooms", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Email",
                keyValue: "schoolhubpl@gmail.com",
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 92, 45, 7, 144, 56, 125, 137, 47, 14, 93, 9, 117, 53, 177, 180, 60, 193, 48, 204, 232, 2, 136, 84, 77, 218, 151, 16, 238, 192, 247, 96, 20, 139, 225, 59, 108, 196, 162, 214, 201, 235, 152, 251, 110, 249, 156, 146, 132, 107, 64, 83, 95, 229, 226, 42, 128, 110, 15, 70, 93, 84, 153, 173, 86 }, new byte[] { 70, 189, 82, 28, 225, 164, 199, 181, 246, 73, 240, 97, 130, 158, 142, 87, 135, 185, 120, 142, 7, 238, 168, 229, 233, 38, 66, 117, 23, 0, 68, 192, 182, 251, 85, 23, 115, 209, 204, 42, 212, 163, 230, 185, 208, 23, 237, 164, 123, 59, 252, 251, 105, 202, 175, 89, 41, 179, 17, 236, 128, 47, 214, 209, 109, 172, 247, 252, 72, 249, 202, 76, 149, 75, 15, 144, 233, 9, 91, 65, 160, 96, 200, 246, 21, 12, 127, 181, 97, 83, 203, 162, 250, 185, 31, 249, 131, 151, 217, 10, 72, 231, 172, 42, 38, 238, 77, 183, 79, 121, 67, 9, 26, 254, 242, 92, 98, 201, 52, 59, 56, 188, 74, 103, 243, 71, 138, 51 } });

            migrationBuilder.CreateIndex(
                name: "IX_Pupils_ClassroomId",
                table: "Pupils",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_ClassAccessCode",
                table: "Classrooms",
                column: "ClassAccessCode",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pupils_Classrooms_ClassroomId",
                table: "Pupils",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pupils_Classrooms_ClassroomId",
                table: "Pupils");

            migrationBuilder.DropTable(
                name: "Classrooms");

            migrationBuilder.DropIndex(
                name: "IX_Pupils_ClassroomId",
                table: "Pupils");

            migrationBuilder.DropColumn(
                name: "ClassroomId",
                table: "Pupils");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Email",
                keyValue: "schoolhubpl@gmail.com",
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 201, 152, 95, 29, 178, 101, 170, 219, 202, 203, 107, 94, 90, 12, 5, 30, 132, 102, 199, 151, 235, 36, 134, 250, 20, 196, 46, 114, 80, 11, 3, 237, 96, 104, 234, 106, 204, 72, 30, 126, 217, 44, 6, 238, 60, 188, 46, 211, 148, 177, 20, 193, 90, 239, 58, 73, 212, 241, 183, 254, 243, 121, 122, 254 }, new byte[] { 97, 101, 182, 101, 161, 142, 231, 28, 215, 166, 92, 202, 216, 190, 70, 94, 134, 144, 38, 248, 154, 183, 13, 228, 38, 82, 24, 78, 74, 168, 254, 239, 70, 79, 74, 230, 99, 18, 165, 205, 120, 62, 65, 151, 105, 167, 207, 250, 36, 56, 134, 68, 211, 36, 165, 141, 125, 148, 116, 206, 28, 120, 181, 73, 106, 20, 137, 62, 243, 90, 13, 35, 48, 61, 75, 205, 254, 87, 38, 206, 30, 5, 76, 173, 76, 70, 31, 204, 15, 8, 84, 111, 165, 143, 35, 88, 7, 34, 146, 123, 242, 39, 195, 180, 202, 197, 2, 22, 89, 46, 130, 112, 26, 251, 53, 172, 181, 137, 93, 176, 168, 114, 62, 115, 107, 226, 94, 154 } });
        }
    }
}
