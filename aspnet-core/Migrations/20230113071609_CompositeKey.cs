using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookProject.Migrations
{
    /// <inheritdoc />
    public partial class CompositeKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookPages_PageId",
                table: "BookPages");

            migrationBuilder.DropIndex(
                name: "IX_BookPages_BookId",
                table: "BookPages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookPages",
                table: "BookPages",
                columns: new[] { "BookId", "PageNumber" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookPages",
                table: "BookPages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookPages_PageId",
                table: "BookPages",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_BookPages_BookId",
                table: "BookPages",
                column: "BookId");
        }
    }
}
