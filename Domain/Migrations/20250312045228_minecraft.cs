using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Domain.Migrations
{
    public partial class minecraft : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PledgeDetails_PaymentLinkInformations_PaymentLinkInformatio~",
                table: "PledgeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PledgeDetails_Pledges_PledgeUserId_PledgeProjectId",
                table: "PledgeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PostComment_Comments_CommentId",
                table: "PostComment");

            migrationBuilder.DropForeignKey(
                name: "FK_PostComment_Posts_PostId",
                table: "PostComment");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "PaymentLinkInformations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pledges",
                table: "Pledges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PledgeDetails",
                table: "PledgeDetails");

            migrationBuilder.DropIndex(
                name: "IX_PledgeDetails_PaymentLinkInformationId",
                table: "PledgeDetails");

            migrationBuilder.DropIndex(
                name: "IX_PledgeDetails_PledgeUserId_PledgeProjectId",
                table: "PledgeDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostComment",
                table: "PostComment");

            migrationBuilder.DropColumn(
                name: "PaymentLinkInformationId",
                table: "PledgeDetails");

            migrationBuilder.DropColumn(
                name: "PledgeProjectId",
                table: "PledgeDetails");

            migrationBuilder.DropColumn(
                name: "dislike",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "like",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "PostComment",
                newName: "PostComments");

            migrationBuilder.RenameColumn(
                name: "PledgeUserId",
                table: "PledgeDetails",
                newName: "PaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_PostComment_PostId",
                table: "PostComments",
                newName: "IX_PostComments_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_PostComment_CommentId",
                table: "PostComments",
                newName: "IX_PostComments_CommentId");

            migrationBuilder.AlterColumn<int>(
                name: "PledgeId",
                table: "Pledges",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "PledgeDetails",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ParentCommentId",
                table: "Comments",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Comments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pledges",
                table: "Pledges",
                column: "PledgeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PledgeDetails",
                table: "PledgeDetails",
                columns: new[] { "PledgeId", "PaymentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostComments",
                table: "PostComments",
                columns: new[] { "CommentId", "PostId" });

            migrationBuilder.CreateIndex(
                name: "IX_Pledges_UserId",
                table: "Pledges",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PledgeDetails_Pledges_PledgeId",
                table: "PledgeDetails",
                column: "PledgeId",
                principalTable: "Pledges",
                principalColumn: "PledgeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostComments_Comments_CommentId",
                table: "PostComments",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostComments_Posts_PostId",
                table: "PostComments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PledgeDetails_Pledges_PledgeId",
                table: "PledgeDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PostComments_Comments_CommentId",
                table: "PostComments");

            migrationBuilder.DropForeignKey(
                name: "FK_PostComments_Posts_PostId",
                table: "PostComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pledges",
                table: "Pledges");

            migrationBuilder.DropIndex(
                name: "IX_Pledges_UserId",
                table: "Pledges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PledgeDetails",
                table: "PledgeDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostComments",
                table: "PostComments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PledgeDetails");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "PostComments",
                newName: "PostComment");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "PledgeDetails",
                newName: "PledgeUserId");

            migrationBuilder.RenameIndex(
                name: "IX_PostComments_PostId",
                table: "PostComment",
                newName: "IX_PostComment_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_PostComments_CommentId",
                table: "PostComment",
                newName: "IX_PostComment_CommentId");

            migrationBuilder.AlterColumn<int>(
                name: "PledgeId",
                table: "Pledges",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "PaymentLinkInformationId",
                table: "PledgeDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PledgeProjectId",
                table: "PledgeDetails",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ParentCommentId",
                table: "Comments",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "dislike",
                table: "Comments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "like",
                table: "Comments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pledges",
                table: "Pledges",
                columns: new[] { "UserId", "ProjectId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PledgeDetails",
                table: "PledgeDetails",
                columns: new[] { "PledgeId", "PaymentLinkInformationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostComment",
                table: "PostComment",
                columns: new[] { "CommentId", "PostId" });

            migrationBuilder.CreateTable(
                name: "PaymentLinkInformations",
                columns: table => new
                {
                    PaymentLinkInformationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    AmountPaid = table.Column<int>(type: "integer", nullable: false),
                    AmountRemaining = table.Column<int>(type: "integer", nullable: false),
                    CancelAt = table.Column<string>(type: "text", nullable: true),
                    CancellationReason = table.Column<string>(type: "text", nullable: true),
                    CreateAt = table.Column<string>(type: "text", nullable: false),
                    Id = table.Column<string>(type: "text", nullable: true),
                    OrderCode = table.Column<long>(type: "bigint", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentLinkInformations", x => x.PaymentLinkInformationId);
                    table.ForeignKey(
                        name: "FK_PaymentLinkInformations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PaymentLinkInformationId = table.Column<int>(type: "integer", nullable: false),
                    AccountNumber = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    CounterAccountBankId = table.Column<string>(type: "text", nullable: true),
                    CounterAccountBankName = table.Column<string>(type: "text", nullable: true),
                    CounterAccountName = table.Column<string>(type: "text", nullable: true),
                    CounterAccountNumber = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Reference = table.Column<string>(type: "text", nullable: false),
                    TransactionDateTime = table.Column<string>(type: "text", nullable: false),
                    VirtualAccountName = table.Column<string>(type: "text", nullable: true),
                    VirtualAccountNumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_PaymentLinkInformations_PaymentLinkInformation~",
                        column: x => x.PaymentLinkInformationId,
                        principalTable: "PaymentLinkInformations",
                        principalColumn: "PaymentLinkInformationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PledgeDetails_PaymentLinkInformationId",
                table: "PledgeDetails",
                column: "PaymentLinkInformationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PledgeDetails_PledgeUserId_PledgeProjectId",
                table: "PledgeDetails",
                columns: new[] { "PledgeUserId", "PledgeProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentLinkInformations_UserId",
                table: "PaymentLinkInformations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PaymentLinkInformationId",
                table: "Transactions",
                column: "PaymentLinkInformationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PledgeDetails_PaymentLinkInformations_PaymentLinkInformatio~",
                table: "PledgeDetails",
                column: "PaymentLinkInformationId",
                principalTable: "PaymentLinkInformations",
                principalColumn: "PaymentLinkInformationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PledgeDetails_Pledges_PledgeUserId_PledgeProjectId",
                table: "PledgeDetails",
                columns: new[] { "PledgeUserId", "PledgeProjectId" },
                principalTable: "Pledges",
                principalColumns: new[] { "UserId", "ProjectId" },
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostComment_Comments_CommentId",
                table: "PostComment",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostComment_Posts_PostId",
                table: "PostComment",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
