using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.MVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class LibraryMVC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Loans",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Loans");
        }
    }
}
