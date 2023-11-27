using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookTrackingSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class addtransactiontable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "remark",
                table: "BorrowBookRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "transactionHistories",
                columns: table => new
                {
                    transID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    borrowerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    libraryCardNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bookName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    borrowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    actualReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactionHistories", x => x.transID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transactionHistories");

            migrationBuilder.AlterColumn<string>(
                name: "remark",
                table: "BorrowBookRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
