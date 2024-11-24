using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyWebsite.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Update_Table_Company : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Slogan",
                schema: "dbo",
                table: "CompanyInfo",
                newName: "ZaloOaId");

            migrationBuilder.AlterColumn<string>(
                name: "FoundationYear",
                schema: "dbo",
                table: "CompanyInfo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManagedBy",
                schema: "dbo",
                table: "CompanyInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeOfBusiness",
                schema: "dbo",
                table: "CompanyInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhatsAppNumber",
                schema: "dbo",
                table: "CompanyInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZaloNumber",
                schema: "dbo",
                table: "CompanyInfo",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagedBy",
                schema: "dbo",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "TypeOfBusiness",
                schema: "dbo",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "WhatsAppNumber",
                schema: "dbo",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "ZaloNumber",
                schema: "dbo",
                table: "CompanyInfo");

            migrationBuilder.RenameColumn(
                name: "ZaloOaId",
                schema: "dbo",
                table: "CompanyInfo",
                newName: "Slogan");

            migrationBuilder.AlterColumn<int>(
                name: "FoundationYear",
                schema: "dbo",
                table: "CompanyInfo",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
