using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookTrackingSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class addreturntable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReturnLists",
                columns: table => new
                {
                    returnID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    borrowID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    bookID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bookName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    libraryCardNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    borrowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    expectReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    remark = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnLists", x => x.returnID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReturnLists");
        }
    }
}
