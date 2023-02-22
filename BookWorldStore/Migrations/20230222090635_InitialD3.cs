using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookWorldStore.Migrations
{
    public partial class InitialD3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "requirements");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "requirements",
                columns: table => new
                {
                    req_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cate_id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requirements", x => x.req_id);
                    table.ForeignKey(
                        name: "FK_requirements_categories_cate_id",
                        column: x => x.cate_id,
                        principalTable: "categories",
                        principalColumn: "cate_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_requirements_cate_id",
                table: "requirements",
                column: "cate_id");
        }
    }
}
