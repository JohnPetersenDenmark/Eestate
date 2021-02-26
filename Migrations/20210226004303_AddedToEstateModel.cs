using Microsoft.EntityFrameworkCore.Migrations;

namespace Eestate.Migrations
{
    public partial class AddedToEstateModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address1",
                table: "Estates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address2",
                table: "Estates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Areal",
                table: "Estates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Estates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EjerudgiftPrMd",
                table: "Estates",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "EstateType",
                table: "Estates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GrundAreal",
                table: "Estates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Liggetid",
                table: "Estates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Estates",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PrisPrM2",
                table: "Estates",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "VaegtetAreal",
                table: "Estates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zip",
                table: "Estates",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address1",
                table: "Estates");

            migrationBuilder.DropColumn(
                name: "Address2",
                table: "Estates");

            migrationBuilder.DropColumn(
                name: "Areal",
                table: "Estates");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Estates");

            migrationBuilder.DropColumn(
                name: "EjerudgiftPrMd",
                table: "Estates");

            migrationBuilder.DropColumn(
                name: "EstateType",
                table: "Estates");

            migrationBuilder.DropColumn(
                name: "GrundAreal",
                table: "Estates");

            migrationBuilder.DropColumn(
                name: "Liggetid",
                table: "Estates");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Estates");

            migrationBuilder.DropColumn(
                name: "PrisPrM2",
                table: "Estates");

            migrationBuilder.DropColumn(
                name: "VaegtetAreal",
                table: "Estates");

            migrationBuilder.DropColumn(
                name: "Zip",
                table: "Estates");
        }
    }
}
