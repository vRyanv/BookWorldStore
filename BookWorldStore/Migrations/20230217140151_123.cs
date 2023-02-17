using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookWorldStore.Migrations
{
    public partial class _123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "token_reset_pass",
                table: "users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "suppliers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "token_reset_pass",
                table: "users");

            migrationBuilder.DropColumn(
                name: "address",
                table: "suppliers");
        }
    }
}
