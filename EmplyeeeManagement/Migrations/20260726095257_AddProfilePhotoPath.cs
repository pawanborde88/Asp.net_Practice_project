using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmplyeeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddProfilePhotoPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "profile_photo_path",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "profile_photo_path",
                table: "Employees");
        }
    }
}
