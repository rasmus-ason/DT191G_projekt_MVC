using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DT191G_projekt.Migrations.CustomerOrder
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerOrder",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Firstname = table.Column<string>(type: "TEXT", nullable: true),
                    Lastname = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Phonenumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Adress = table.Column<string>(type: "TEXT", nullable: true),
                    ZipCode = table.Column<int>(type: "INTEGER", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    TotalPrice = table.Column<double>(type: "REAL", nullable: true),
                    ShippingCost = table.Column<int>(type: "INTEGER", nullable: true),
                    IsPacked = table.Column<bool>(type: "INTEGER", nullable: true),
                    IsShipped = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrder", x => x.OrderId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerOrder");
        }
    }
}
