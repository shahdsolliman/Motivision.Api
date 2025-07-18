using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Motivision.Infrastructure.Persistence.Migrations
{
    public partial class Add_IsCompleted_IsInterrupted_Mode_To_FocusSession : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "FocusSessions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsInterrupted",
                table: "FocusSessions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Mode",
                table: "FocusSessions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "FocusSessions");

            migrationBuilder.DropColumn(
                name: "IsInterrupted",
                table: "FocusSessions");

            migrationBuilder.DropColumn(
                name: "Mode",
                table: "FocusSessions");
        }
    }
}
