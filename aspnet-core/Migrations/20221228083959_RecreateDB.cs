using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookProject.Migrations
{
    /// <inheritdoc />
    public partial class RecreateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                },
                comment: "Các loại sách");

            migrationBuilder.CreateTable(
                name: "BookInfo",
                columns: table => new
                {
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "ID sách"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "Tên/ Tựa đề sách"),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "Tác giả"),
                    ContentType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "smalldatetime", nullable: true, comment: "Thời gian tạo"),
                    ModifiedDate = table.Column<DateTime>(type: "smalldatetime", nullable: true, comment: "Thời gian chỉnh sửa")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookInfo_BookId", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_BookInfo_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookPages",
                columns: table => new
                {
                    PageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PageNumber = table.Column<short>(type: "smallint", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookPages_PageId", x => x.PageId);
                    table.ForeignKey(
                        name: "FK_BookPages_BookId",
                        column: x => x.BookId,
                        principalTable: "BookInfo",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Các trang sách");

            migrationBuilder.CreateIndex(
                name: "IX_BookInfo_CategoryId",
                table: "BookInfo",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BookPages_BookId",
                table: "BookPages",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookPages");

            migrationBuilder.DropTable(
                name: "BookInfo");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
