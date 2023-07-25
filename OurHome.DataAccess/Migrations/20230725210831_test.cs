using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OurHome.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Bills_BillID",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillCoOwners",
                table: "BillCoOwners");

            migrationBuilder.DropIndex(
                name: "IX_BillCoOwners_BillID",
                table: "BillCoOwners");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BillID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "BillCoOwners");

            migrationBuilder.DropColumn(
                name: "BillID",
                table: "AspNetUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillCoOwners",
                table: "BillCoOwners",
                columns: new[] { "BillID", "UserID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BillCoOwners",
                table: "BillCoOwners");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "BillCoOwners",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "BillID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillCoOwners",
                table: "BillCoOwners",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_BillCoOwners_BillID",
                table: "BillCoOwners",
                column: "BillID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BillID",
                table: "AspNetUsers",
                column: "BillID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Bills_BillID",
                table: "AspNetUsers",
                column: "BillID",
                principalTable: "Bills",
                principalColumn: "ID");
        }
    }
}
