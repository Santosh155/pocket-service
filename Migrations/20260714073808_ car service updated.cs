using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pocket_service.Migrations
{
    /// <inheritdoc />
    public partial class carserviceupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "CarServices",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CarServices_UserId",
                table: "CarServices",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarServices_Users_UserId",
                table: "CarServices",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarServices_Users_UserId",
                table: "CarServices");

            migrationBuilder.DropIndex(
                name: "IX_CarServices_UserId",
                table: "CarServices");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CarServices");
        }
    }
}
