using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookWorldStore.Migrations
{
    public partial class _7thMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_books_book_id",
                table: "OrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_orders_order_id",
                table: "OrderDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail");

            migrationBuilder.RenameTable(
                name: "OrderDetail",
                newName: "orderDetails");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetail_order_id",
                table: "orderDetails",
                newName: "IX_orderDetails_order_id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetail_book_id",
                table: "orderDetails",
                newName: "IX_orderDetails_book_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_orderDetails",
                table: "orderDetails",
                column: "order_detail_id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_orderDetails_books_book_id",
                table: "orderDetails",
                column: "book_id",
                principalTable: "books",
                principalColumn: "book_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orderDetails_orders_order_id",
                table: "orderDetails",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "order_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderDetails_books_book_id",
                table: "orderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_orderDetails_orders_order_id",
                table: "orderDetails");

            migrationBuilder.DropTable(
                name: "requirements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_orderDetails",
                table: "orderDetails");

            migrationBuilder.RenameTable(
                name: "orderDetails",
                newName: "OrderDetail");

            migrationBuilder.RenameIndex(
                name: "IX_orderDetails_order_id",
                table: "OrderDetail",
                newName: "IX_OrderDetail_order_id");

            migrationBuilder.RenameIndex(
                name: "IX_orderDetails_book_id",
                table: "OrderDetail",
                newName: "IX_OrderDetail_book_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail",
                column: "order_detail_id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_books_book_id",
                table: "OrderDetail",
                column: "book_id",
                principalTable: "books",
                principalColumn: "book_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_orders_order_id",
                table: "OrderDetail",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "order_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
