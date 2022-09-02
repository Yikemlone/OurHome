using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OurHome.Server.Migrations
{
    public partial class intialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bill = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PersonsBills",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Rent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Internet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Bins = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Electricity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Milk = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Oil = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonsBills", x => x.Name);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "PersonsBills");
        }
    }
}
