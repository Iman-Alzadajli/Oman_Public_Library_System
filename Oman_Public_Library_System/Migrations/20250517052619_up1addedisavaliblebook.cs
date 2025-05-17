using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oman_Public_Library_System.Migrations
{
    /// <inheritdoc />
    public partial class up1addedisavaliblebook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IsAvailable",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Books");
        }
    }
}
