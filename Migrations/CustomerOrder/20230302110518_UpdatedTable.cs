using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DT191G_projekt.Migrations.CustomerOrder
{
    /// <inheritdoc />
    public partial class UpdatedTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "CustomerOrder",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ShippingCost",
                table: "CustomerOrder",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Phonenumber",
                table: "CustomerOrder",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "CustomerOrder",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "ShippingCost",
                table: "CustomerOrder",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Phonenumber",
                table: "CustomerOrder",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");
        }
    }
}
