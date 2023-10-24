using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolHubApi.Migrations
{
    /// <inheritdoc />
    public partial class ContentTypePlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PlanContentType",
                table: "Classrooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Email",
                keyValue: "schoolhubpl@gmail.com",
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 67, 58, 59, 74, 111, 168, 106, 72, 90, 93, 165, 77, 156, 118, 146, 133, 212, 110, 21, 201, 216, 155, 71, 218, 156, 24, 245, 211, 159, 105, 50, 126, 15, 250, 32, 229, 10, 166, 84, 94, 86, 20, 138, 95, 120, 62, 149, 79, 243, 191, 6, 6, 94, 95, 81, 145, 32, 184, 72, 69, 56, 41, 96, 212 }, new byte[] { 143, 231, 28, 232, 17, 148, 76, 250, 96, 174, 189, 1, 143, 221, 198, 223, 231, 199, 21, 116, 122, 50, 121, 104, 98, 67, 148, 114, 226, 134, 238, 96, 182, 125, 49, 31, 230, 198, 86, 233, 157, 55, 158, 105, 205, 104, 47, 157, 96, 175, 108, 149, 85, 208, 118, 129, 87, 176, 142, 38, 182, 237, 120, 5, 111, 120, 126, 89, 208, 225, 6, 233, 150, 245, 95, 72, 137, 36, 142, 60, 7, 65, 246, 248, 212, 143, 235, 120, 135, 117, 157, 70, 201, 28, 220, 89, 254, 80, 140, 101, 59, 178, 111, 128, 44, 83, 46, 59, 69, 182, 34, 184, 69, 156, 160, 32, 108, 186, 102, 78, 242, 143, 151, 239, 78, 149, 170, 184 } });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlanContentType",
                table: "Classrooms");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Email",
                keyValue: "schoolhubpl@gmail.com",
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 68, 13, 80, 156, 26, 43, 144, 54, 176, 108, 77, 11, 238, 69, 24, 89, 100, 122, 71, 164, 79, 56, 108, 224, 70, 203, 110, 229, 160, 215, 58, 20, 47, 220, 34, 72, 234, 100, 104, 148, 146, 211, 200, 206, 132, 79, 8, 92, 46, 173, 141, 16, 135, 132, 35, 166, 29, 116, 60, 250, 67, 53, 137, 106 }, new byte[] { 157, 133, 209, 5, 189, 32, 225, 246, 178, 220, 182, 117, 16, 214, 158, 29, 212, 181, 254, 201, 8, 214, 52, 15, 152, 92, 182, 102, 254, 76, 16, 234, 104, 125, 102, 76, 29, 31, 175, 81, 97, 52, 245, 4, 182, 46, 65, 90, 176, 164, 206, 45, 131, 38, 12, 76, 83, 186, 180, 188, 120, 195, 86, 80, 180, 24, 47, 71, 169, 236, 152, 192, 47, 134, 112, 205, 37, 52, 49, 170, 140, 178, 96, 226, 207, 43, 176, 226, 167, 81, 41, 100, 183, 38, 131, 171, 213, 215, 69, 36, 34, 121, 215, 148, 183, 1, 253, 11, 141, 17, 229, 151, 227, 42, 175, 56, 142, 70, 178, 130, 200, 214, 71, 43, 104, 1, 135, 219 } });
        }
    }
}
