using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookProject.Migrations
{
    /// <inheritdoc />
    public partial class AddProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfPages",
                table: "BookInfo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "Số trang");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfPages",
                table: "BookInfo");
        }
    }
}
