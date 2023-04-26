using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warehouse.Api.Migrations
{
    public partial class GoodsIssueBasketStorageSlotFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsIssueEntryBasket_StorageSlots_StorageSlotId",
                table: "GoodsIssueEntryBasket");

            migrationBuilder.DropIndex(
                name: "IX_GoodsIssueEntryBasket_StorageSlotId",
                table: "GoodsIssueEntryBasket");

            migrationBuilder.AlterColumn<string>(
                name: "StorageSlotId",
                table: "GoodsIssueEntryBasket",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StorageSlotId",
                table: "GoodsIssueEntryBasket",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsIssueEntryBasket_StorageSlotId",
                table: "GoodsIssueEntryBasket",
                column: "StorageSlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsIssueEntryBasket_StorageSlots_StorageSlotId",
                table: "GoodsIssueEntryBasket",
                column: "StorageSlotId",
                principalTable: "StorageSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
