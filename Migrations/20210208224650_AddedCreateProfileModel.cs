using Microsoft.EntityFrameworkCore.Migrations;

namespace Eestate.Migrations
{
    public partial class AddedCreateProfileModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmPassword",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "IdentityUserId",
                table: "Profiles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfirmPassword",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserId",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
