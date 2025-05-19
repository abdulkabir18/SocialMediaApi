using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IgnoreColmn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Chats");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Friends",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Friends",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Chats",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Chats",
                type: "longtext",
                nullable: true);
        }
    }
}
