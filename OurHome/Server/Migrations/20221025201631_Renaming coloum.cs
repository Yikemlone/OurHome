using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OurHome.Server.Migrations
{
    public partial class Renamingcoloum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PersonID",
                table: "BillsDueDate",
                newName: "BillID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BillID",
                table: "BillsDueDate",
                newName: "PersonID");
        }
    }
}
