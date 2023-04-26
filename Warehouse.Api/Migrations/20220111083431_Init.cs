using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warehouse.Api.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "warehouse");

            migrationBuilder.CreateSequence(
                name: "basketeq",
                schema: "warehouse",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "basketInconsistencieeq",
                schema: "warehouse",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "employeeeq",
                schema: "warehouse",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "goodsIssueEntriyeq",
                schema: "warehouse",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "goodsissueentrybasketeq",
                schema: "warehouse",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "goodsIssueeq",
                schema: "warehouse",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "goodsreceiptentryeq",
                schema: "warehouse",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "goodsreceipteq",
                schema: "warehouse",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "producteq",
                schema: "warehouse",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "stockcardentryeq",
                schema: "warehouse",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "storagesloteq",
                schema: "warehouse",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PiecesPerKilogram = table.Column<double>(type: "float", nullable: false),
                    UnitOfMeasurement = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StorageSlots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    StorageSlotId = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageSlots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GoodsIssues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    GoodsIssueId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShiftLeaderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsIssues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsIssues_Employees_ShiftLeaderId",
                        column: x => x.ShiftLeaderId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodsReceipts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceipts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsReceipts_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BasketInconsistencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BasketId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StorageSlotId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    GoodsIssueId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentQuantity = table.Column<int>(type: "int", nullable: false),
                    CurrentMass = table.Column<double>(type: "float", nullable: false),
                    NewQuantity = table.Column<int>(type: "int", nullable: false),
                    NewMass = table.Column<double>(type: "float", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsFixed = table.Column<bool>(type: "bit", nullable: false),
                    ReporterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketInconsistencies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketInconsistencies_Employees_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketInconsistencies_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockCardEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BeforeQuantity = table.Column<int>(type: "int", nullable: false),
                    BeforeMass = table.Column<double>(type: "float", nullable: false),
                    InputQuantity = table.Column<int>(type: "int", nullable: false),
                    InputMass = table.Column<double>(type: "float", nullable: false),
                    OutputQuantity = table.Column<int>(type: "int", nullable: false),
                    OutputMass = table.Column<double>(type: "float", nullable: false),
                    AfterQuantity = table.Column<int>(type: "int", nullable: false),
                    AfterMass = table.Column<double>(type: "float", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCardEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockCardEntries_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    BasketId = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    ProductionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StorageSlotId = table.Column<int>(type: "int", nullable: true),
                    PlannedQuantity = table.Column<int>(type: "int", nullable: true),
                    ActualQuantity = table.Column<int>(type: "int", nullable: true),
                    IsConsistent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Baskets_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Baskets_StorageSlots_StorageSlotId",
                        column: x => x.StorageSlotId,
                        principalTable: "StorageSlots",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GoodsIssueEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    GoodsIssueId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    TotalQuantity = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsIssueEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsIssueEntry_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodsIssueEntry_GoodsIssues_GoodsIssueId",
                        column: x => x.GoodsIssueId,
                        principalTable: "GoodsIssues",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GoodsIssueEntry_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodsReceiptEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    GoodsReceiptId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    BasketId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlannedQuantity = table.Column<int>(type: "int", nullable: false),
                    ActualQuantity = table.Column<int>(type: "int", nullable: false),
                    ProductionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsReceiptEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsReceiptEntry_GoodsReceipts_GoodsReceiptId",
                        column: x => x.GoodsReceiptId,
                        principalTable: "GoodsReceipts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodsReceiptEntry_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodsIssueEntryBasket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    GoodsIssueEntryId = table.Column<int>(type: "int", nullable: false),
                    BasketId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StorageSlotId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Mass = table.Column<double>(type: "float", nullable: false),
                    ProductionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsTaken = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsIssueEntryBasket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsIssueEntryBasket_GoodsIssueEntry_GoodsIssueEntryId",
                        column: x => x.GoodsIssueEntryId,
                        principalTable: "GoodsIssueEntry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodsIssueEntryBasket_StorageSlots_StorageSlotId",
                        column: x => x.StorageSlotId,
                        principalTable: "StorageSlots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketInconsistencies_BasketId_Timestamp",
                table: "BasketInconsistencies",
                columns: new[] { "BasketId", "Timestamp" });

            migrationBuilder.CreateIndex(
                name: "IX_BasketInconsistencies_ProductId",
                table: "BasketInconsistencies",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketInconsistencies_ReporterId",
                table: "BasketInconsistencies",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_BasketId",
                table: "Baskets",
                column: "BasketId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_ProductId",
                table: "Baskets",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_StorageSlotId",
                table: "Baskets",
                column: "StorageSlotId",
                unique: true,
                filter: "[StorageSlotId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeId",
                table: "Employees",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsIssueEntry_EmployeeId",
                table: "GoodsIssueEntry",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsIssueEntry_GoodsIssueId_ProductId",
                table: "GoodsIssueEntry",
                columns: new[] { "GoodsIssueId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsIssueEntry_ProductId",
                table: "GoodsIssueEntry",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsIssueEntryBasket_GoodsIssueEntryId_BasketId",
                table: "GoodsIssueEntryBasket",
                columns: new[] { "GoodsIssueEntryId", "BasketId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsIssueEntryBasket_StorageSlotId",
                table: "GoodsIssueEntryBasket",
                column: "StorageSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsIssues_GoodsIssueId",
                table: "GoodsIssues",
                column: "GoodsIssueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsIssues_ShiftLeaderId",
                table: "GoodsIssues",
                column: "ShiftLeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceiptEntry_GoodsReceiptId_BasketId",
                table: "GoodsReceiptEntry",
                columns: new[] { "GoodsReceiptId", "BasketId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceiptEntry_ProductId",
                table: "GoodsReceiptEntry",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceipts_EmployeeId",
                table: "GoodsReceipts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceipts_Timestamp",
                table: "GoodsReceipts",
                column: "Timestamp",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductId",
                table: "Products",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockCardEntries_ProductId_Date",
                table: "StockCardEntries",
                columns: new[] { "ProductId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StorageSlots_StorageSlotId",
                table: "StorageSlots",
                column: "StorageSlotId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketInconsistencies");

            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.DropTable(
                name: "GoodsIssueEntryBasket");

            migrationBuilder.DropTable(
                name: "GoodsReceiptEntry");

            migrationBuilder.DropTable(
                name: "StockCardEntries");

            migrationBuilder.DropTable(
                name: "GoodsIssueEntry");

            migrationBuilder.DropTable(
                name: "StorageSlots");

            migrationBuilder.DropTable(
                name: "GoodsReceipts");

            migrationBuilder.DropTable(
                name: "GoodsIssues");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropSequence(
                name: "basketeq",
                schema: "warehouse");

            migrationBuilder.DropSequence(
                name: "basketInconsistencieeq",
                schema: "warehouse");

            migrationBuilder.DropSequence(
                name: "employeeeq",
                schema: "warehouse");

            migrationBuilder.DropSequence(
                name: "goodsIssueEntriyeq",
                schema: "warehouse");

            migrationBuilder.DropSequence(
                name: "goodsissueentrybasketeq",
                schema: "warehouse");

            migrationBuilder.DropSequence(
                name: "goodsIssueeq",
                schema: "warehouse");

            migrationBuilder.DropSequence(
                name: "goodsreceiptentryeq",
                schema: "warehouse");

            migrationBuilder.DropSequence(
                name: "goodsreceipteq",
                schema: "warehouse");

            migrationBuilder.DropSequence(
                name: "producteq",
                schema: "warehouse");

            migrationBuilder.DropSequence(
                name: "stockcardentryeq",
                schema: "warehouse");

            migrationBuilder.DropSequence(
                name: "storagesloteq",
                schema: "warehouse");
        }
    }
}
