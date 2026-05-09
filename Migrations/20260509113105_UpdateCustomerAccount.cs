using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCustomerAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountStatus",
                table: "CustomerAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OwnerShipType",
                table: "CustomerAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountStatus",
                table: "CustomerAccounts");

            migrationBuilder.DropColumn(
                name: "OwnerShipType",
                table: "CustomerAccounts");
        }
    }
}
