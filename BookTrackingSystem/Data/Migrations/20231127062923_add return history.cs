using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookTrackingSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class addreturnhistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "returnHistories",
                columns: table => new
                {
                    returnTransID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    borrowID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    borrowerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    libraryCardNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bookName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    borrowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    actualReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_returnHistories", x => x.returnTransID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "returnHistories");
        }
    }
}
