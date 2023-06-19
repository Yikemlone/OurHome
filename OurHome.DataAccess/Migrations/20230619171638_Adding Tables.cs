using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OurHome.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddingTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PendingApproval",
                table: "UserBills",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PersonalNote",
                table: "UserBills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "UserPrice",
                table: "UserBills",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HomeID",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "SplitBill",
                table: "Bills",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "UserID",
                table: "Bills",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "BillCoOwners",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BillID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillCoOwners", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BillCoOwners_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillCoOwners_Bills_BillID",
                        column: x => x.BillID,
                        principalTable: "Bills",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Homes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Homes_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HomeBills",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BillName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PriceVaries = table.Column<bool>(type: "bit", nullable: false),
                    HomeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeBills", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HomeBills_Homes_HomeID",
                        column: x => x.HomeID,
                        principalTable: "Homes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HomeUsers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HomeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeUsers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_HomeUsers_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HomeUsers_Homes_HomeID",
                        column: x => x.HomeID,
                        principalTable: "Homes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToUserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HomeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Invations_AspNetUsers_FromUserID",
                        column: x => x.FromUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invations_AspNetUsers_ToUserID",
                        column: x => x.ToUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invations_Homes_HomeID",
                        column: x => x.HomeID,
                        principalTable: "Homes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_HomeID",
                table: "Bills",
                column: "HomeID");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_UserID",
                table: "Bills",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_BillCoOwners_BillID",
                table: "BillCoOwners",
                column: "BillID");

            migrationBuilder.CreateIndex(
                name: "IX_BillCoOwners_UserID",
                table: "BillCoOwners",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_HomeBills_HomeID",
                table: "HomeBills",
                column: "HomeID");

            migrationBuilder.CreateIndex(
                name: "IX_Homes_UserID",
                table: "Homes",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_HomeUsers_HomeID",
                table: "HomeUsers",
                column: "HomeID");

            migrationBuilder.CreateIndex(
                name: "IX_HomeUsers_UserID",
                table: "HomeUsers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Invations_FromUserID",
                table: "Invations",
                column: "FromUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Invations_HomeID",
                table: "Invations",
                column: "HomeID");

            migrationBuilder.CreateIndex(
                name: "IX_Invations_ToUserID",
                table: "Invations",
                column: "ToUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_AspNetUsers_UserID",
                table: "Bills",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Homes_HomeID",
                table: "Bills",
                column: "HomeID",
                principalTable: "Homes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_AspNetUsers_UserID",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Homes_HomeID",
                table: "Bills");

            migrationBuilder.DropTable(
                name: "BillCoOwners");

            migrationBuilder.DropTable(
                name: "HomeBills");

            migrationBuilder.DropTable(
                name: "HomeUsers");

            migrationBuilder.DropTable(
                name: "Invations");

            migrationBuilder.DropTable(
                name: "Homes");

            migrationBuilder.DropIndex(
                name: "IX_Bills_HomeID",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_UserID",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "PendingApproval",
                table: "UserBills");

            migrationBuilder.DropColumn(
                name: "PersonalNote",
                table: "UserBills");

            migrationBuilder.DropColumn(
                name: "UserPrice",
                table: "UserBills");

            migrationBuilder.DropColumn(
                name: "HomeID",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "SplitBill",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Bills");
        }
    }
}
