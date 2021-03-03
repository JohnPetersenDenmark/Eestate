using Microsoft.EntityFrameworkCore.Migrations;

namespace Eestate.Migrations
{
    public partial class addedDocumntTypeToAttachmentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DocumentTypeId",
                table: "FileAttachments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentTypeId",
                table: "FileAttachments");
        }
    }
}
