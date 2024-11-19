using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWebsite.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Add_ShortName_CompanyInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                schema: "dbo",
                table: "CompanyInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortName",
                schema: "dbo",
                table: "CompanyInfo");
        }
    }
}
