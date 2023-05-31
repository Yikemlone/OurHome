using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OurHome.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class IDupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserID",
                table: "UserBills",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_UserBills_UserID",
                table: "UserBills",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBills_AspNetUsers_UserID",
                table: "UserBills",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBills_AspNetUsers_UserID",
                table: "UserBills");

            migrationBuilder.DropIndex(
                name: "IX_UserBills_UserID",
                table: "UserBills");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "UserBills");
        }
    }
}
