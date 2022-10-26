using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OurHome.Server.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    BillID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bill = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.BillID);
                });

            migrationBuilder.CreateTable(
                name: "BillsDueDate",
                columns: table => new
                {
                    PersonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillDueDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillsDueDate", x => x.PersonID);
                });

            migrationBuilder.CreateTable(
                name: "PastBills",
                columns: table => new
                {
                    PastBillID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillMonth = table.Column<DateTime>(type: "date", nullable: false),
                    Rent = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Internet = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Bins = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Oil = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Electric = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PastBills", x => x.PastBillID);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PersonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonName = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonID);
                });

            migrationBuilder.CreateTable(
                name: "PayedBills",
                columns: table => new
                {
                    PayedBillID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID = table.Column<int>(type: "int", nullable: false),
                    BillDate = table.Column<DateTime>(type: "date", nullable: false),
                    Bill = table.Column<string>(type: "varchar(50)", nullable: false),
                    PaymentType = table.Column<string>(type: "varchar(4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayedBills", x => x.PayedBillID);
                    table.ForeignKey(
                        name: "FK_PayedBills_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonsBills",
                columns: table => new
                {
                    PersonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonID1 = table.Column<int>(type: "int", nullable: false),
                    Rent = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Internet = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Bins = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Electricity = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Milk = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Oil = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonsBills", x => x.PersonID);
                    table.ForeignKey(
                        name: "FK_PersonsBills_Person_PersonID1",
                        column: x => x.PersonID1,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PayedBills_PersonID",
                table: "PayedBills",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonsBills_PersonID1",
                table: "PersonsBills",
                column: "PersonID1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "BillsDueDate");

            migrationBuilder.DropTable(
                name: "PastBills");

            migrationBuilder.DropTable(
                name: "PayedBills");

            migrationBuilder.DropTable(
                name: "PersonsBills");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
