using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceUserManagement.Data.Migrations
{
    public partial class Selectedproperyadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Selected",
                table: "ApiPermissions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Selected",
                table: "ApiPermissions");
        }
    }
}
