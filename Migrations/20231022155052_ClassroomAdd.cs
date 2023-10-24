using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolHubApi.Migrations
{
    /// <inheritdoc />
    public partial class ClassroomAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Plan",
                table: "Classrooms",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Email",
                keyValue: "schoolhubpl@gmail.com",
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 68, 13, 80, 156, 26, 43, 144, 54, 176, 108, 77, 11, 238, 69, 24, 89, 100, 122, 71, 164, 79, 56, 108, 224, 70, 203, 110, 229, 160, 215, 58, 20, 47, 220, 34, 72, 234, 100, 104, 148, 146, 211, 200, 206, 132, 79, 8, 92, 46, 173, 141, 16, 135, 132, 35, 166, 29, 116, 60, 250, 67, 53, 137, 106 }, new byte[] { 157, 133, 209, 5, 189, 32, 225, 246, 178, 220, 182, 117, 16, 214, 158, 29, 212, 181, 254, 201, 8, 214, 52, 15, 152, 92, 182, 102, 254, 76, 16, 234, 104, 125, 102, 76, 29, 31, 175, 81, 97, 52, 245, 4, 182, 46, 65, 90, 176, 164, 206, 45, 131, 38, 12, 76, 83, 186, 180, 188, 120, 195, 86, 80, 180, 24, 47, 71, 169, 236, 152, 192, 47, 134, 112, 205, 37, 52, 49, 170, 140, 178, 96, 226, 207, 43, 176, 226, 167, 81, 41, 100, 183, 38, 131, 171, 213, 215, 69, 36, 34, 121, 215, 148, 183, 1, 253, 11, 141, 17, 229, 151, 227, 42, 175, 56, 142, 70, 178, 130, 200, 214, 71, 43, 104, 1, 135, 219 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Plan",
                table: "Classrooms",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Email",
                keyValue: "schoolhubpl@gmail.com",
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 92, 45, 7, 144, 56, 125, 137, 47, 14, 93, 9, 117, 53, 177, 180, 60, 193, 48, 204, 232, 2, 136, 84, 77, 218, 151, 16, 238, 192, 247, 96, 20, 139, 225, 59, 108, 196, 162, 214, 201, 235, 152, 251, 110, 249, 156, 146, 132, 107, 64, 83, 95, 229, 226, 42, 128, 110, 15, 70, 93, 84, 153, 173, 86 }, new byte[] { 70, 189, 82, 28, 225, 164, 199, 181, 246, 73, 240, 97, 130, 158, 142, 87, 135, 185, 120, 142, 7, 238, 168, 229, 233, 38, 66, 117, 23, 0, 68, 192, 182, 251, 85, 23, 115, 209, 204, 42, 212, 163, 230, 185, 208, 23, 237, 164, 123, 59, 252, 251, 105, 202, 175, 89, 41, 179, 17, 236, 128, 47, 214, 209, 109, 172, 247, 252, 72, 249, 202, 76, 149, 75, 15, 144, 233, 9, 91, 65, 160, 96, 200, 246, 21, 12, 127, 181, 97, 83, 203, 162, 250, 185, 31, 249, 131, 151, 217, 10, 72, 231, 172, 42, 38, 238, 77, 183, 79, 121, 67, 9, 26, 254, 242, 92, 98, 201, 52, 59, 56, 188, 74, 103, 243, 71, 138, 51 } });
        }
    }
}
