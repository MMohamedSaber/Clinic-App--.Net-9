using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.Infrastructure.Data.migs
{
    /// <inheritdoc />
    public partial class LinkUserwithStaffAndEditing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Patients");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Staff",
                type: "nvarchar(450)",
                nullable: true
               );

            migrationBuilder.CreateIndex(
                name: "IX_Staff_AppUserId",
                table: "Staff",
                column: "AppUserId",
                unique: true,
                filter: "[AppUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_AspNetUsers_AppUserId",
                table: "Staff",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staff_AspNetUsers_AppUserId",
                table: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_Staff_AppUserId",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Staff");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Staff",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
