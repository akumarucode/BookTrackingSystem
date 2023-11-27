using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookTrackingSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class addborrowhistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transactionHistories");

            migrationBuilder.CreateTable(
                name: "borrowHistories",
                columns: table => new
                {
                    transID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    borrowID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    borrowerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    libraryCardNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bookName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    borrowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    approvedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_borrowHistories", x => x.transID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "borrowHistories");

            migrationBuilder.CreateTable(
                name: "transactionHistories",
                columns: table => new
                {
                    transID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    actualReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    bookName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    borrowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    borrowerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    libraryCardNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transactionHistories", x => x.transID);
                });
        }
    }
}
