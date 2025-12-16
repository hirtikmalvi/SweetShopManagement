using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SweetShop.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQuantityInStockFieldCheckColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Sweet_QuantityInStock",
                table: "Sweets");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Sweet_QuantityInStock",
                table: "Sweets",
                sql: "[QuantityInStock] >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Sweet_QuantityInStock",
                table: "Sweets");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Sweet_QuantityInStock",
                table: "Sweets",
                sql: "[QuantityInStock] >= 1");
        }
    }
}
