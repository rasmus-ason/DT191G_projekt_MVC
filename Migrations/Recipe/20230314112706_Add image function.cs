using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DT191G_projekt.Migrations.Recipe
{
    /// <inheritdoc />
    public partial class Addimagefunction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AltText",
                table: "Recipe",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Recipe",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AltText",
                table: "Recipe");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Recipe");
        }
    }
}
