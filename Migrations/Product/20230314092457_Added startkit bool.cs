using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DT191G_projekt.Migrations.Product
{
    /// <inheritdoc />
    public partial class Addedstartkitbool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsInStartkit",
                table: "Product",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInStartkit",
                table: "Product");
        }
    }
}
