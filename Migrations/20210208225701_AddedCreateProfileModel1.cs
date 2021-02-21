using Microsoft.EntityFrameworkCore.Migrations;

namespace Eestate.Migrations
{
    public partial class AddedCreateProfileModel1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles");

            migrationBuilder.RenameTable(
                name: "Profiles",
                newName: "CreateProfiles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CreateProfiles",
                table: "CreateProfiles",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CreateProfiles",
                table: "CreateProfiles");

            migrationBuilder.RenameTable(
                name: "CreateProfiles",
                newName: "Profiles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles",
                column: "Id");
        }
    }
}
