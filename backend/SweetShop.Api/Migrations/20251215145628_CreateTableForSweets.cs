using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SweetShop.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableForSweets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sweets",
                columns: table => new
                {
                    SweetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantityInStock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sweets", x => x.SweetId);
                    table.CheckConstraint("CK_Sweet_Price", "[Price] >= 0.0");
                    table.CheckConstraint("CK_Sweet_QuantityInStock", "[QuantityInStock] >= 1");
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sweets");
        }
    }
}
