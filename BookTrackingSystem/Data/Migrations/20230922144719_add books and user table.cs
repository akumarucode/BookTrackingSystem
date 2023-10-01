using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookTrackingSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class addbooksandusertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    bookID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    bookName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    registerTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    issuer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.bookID);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    userID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.userID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
