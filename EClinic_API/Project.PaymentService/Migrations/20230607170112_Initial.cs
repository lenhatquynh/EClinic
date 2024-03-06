using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.PaymentService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    TransactionID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentAmount = table.Column<double>(type: "float", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentService = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentID);
                });

            migrationBuilder.CreateTable(
                name: "Refunds",
                columns: table => new
                {
                    RefundID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    RefundTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefundAmount = table.Column<double>(type: "float", nullable: false),
                    RefundReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Refunds", x => x.RefundID);
                    table.ForeignKey(
                        name: "PK_Payment_One_To_One_Refund",
                        column: x => x.PaymentID,
                        principalTable: "Payments",
                        principalColumn: "PaymentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Refunds_PaymentID",
                table: "Refunds",
                column: "PaymentID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Refunds");

            migrationBuilder.DropTable(
                name: "Payments");
        }
    }
}
