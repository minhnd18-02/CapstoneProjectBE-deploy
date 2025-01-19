using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    public partial class updatePayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentLinkInformation",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaymentLinkInformationId = table.Column<int>(type: "int", nullable: false),
                    OrderCode = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    AmountPaid = table.Column<int>(type: "int", nullable: false),
                    AmountRemaining = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CancelAt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CancellationReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentLinkInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentLinkInformationId = table.Column<int>(type: "int", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionDateTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VirtualAccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VirtualAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CounterAccountBankId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CounterAccountBankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CounterAccountName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CounterAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentLinkInformationId1 = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_PaymentLinkInformation_PaymentLinkInformationId1",
                        column: x => x.PaymentLinkInformationId1,
                        principalTable: "PaymentLinkInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PaymentLinkInformationId1",
                table: "Transactions",
                column: "PaymentLinkInformationId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "PaymentLinkInformation");
        }
    }
}
