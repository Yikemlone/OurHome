using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OurHome.Server.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonsBills_Person_PersonID1",
                table: "PersonsBills");

            migrationBuilder.DropIndex(
                name: "IX_PersonsBills_PersonID1",
                table: "PersonsBills");

            migrationBuilder.DropColumn(
                name: "PersonID1",
                table: "PersonsBills");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonID1",
                table: "PersonsBills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PersonsBills_PersonID1",
                table: "PersonsBills",
                column: "PersonID1");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonsBills_Person_PersonID1",
                table: "PersonsBills",
                column: "PersonID1",
                principalTable: "Person",
                principalColumn: "PersonID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
