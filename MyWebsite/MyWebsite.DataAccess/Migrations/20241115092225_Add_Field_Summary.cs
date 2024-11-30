using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWebsite.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Add_Field_Summary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Summary",
                schema: "dbo",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Summary",
                schema: "dbo",
                table: "Products");
        }
    }
}
