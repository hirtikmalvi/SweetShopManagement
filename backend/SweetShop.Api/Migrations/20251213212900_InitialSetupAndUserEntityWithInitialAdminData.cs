using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SweetShop.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialSetupAndUserEntityWithInitialAdminData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "IsAdmin", "Name", "PasswordHash" },
                values: new object[] { 1, "Hitesh@gmail.com", true, "Hitesh", "AQAAAAIAAYagAAAAEPB00o6RYiRCONfnBM9vh5Vx5U2pwbwKX2fKNd7+R7ewPvx0uklyzeRCoIcVSbsyCA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
