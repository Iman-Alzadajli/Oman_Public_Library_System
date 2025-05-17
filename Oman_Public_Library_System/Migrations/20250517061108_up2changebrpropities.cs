using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oman_Public_Library_System.Migrations
{
    /// <inheritdoc />
    public partial class up2changebrpropities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Return_date",
                table: "Borrow_Records",
                newName: "ReturnDate");

            migrationBuilder.RenameColumn(
                name: "Borrow_date",
                table: "Borrow_Records",
                newName: "BorrowDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReturnDate",
                table: "Borrow_Records",
                newName: "Return_date");

            migrationBuilder.RenameColumn(
                name: "BorrowDate",
                table: "Borrow_Records",
                newName: "Borrow_date");
        }
    }
}
