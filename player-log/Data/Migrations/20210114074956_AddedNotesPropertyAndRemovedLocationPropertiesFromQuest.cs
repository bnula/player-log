using Microsoft.EntityFrameworkCore.Migrations;

namespace player_log.Data.Migrations
{
    public partial class AddedNotesPropertyAndRemovedLocationPropertiesFromQuest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartingLocation",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "StartingLocationId",
                table: "Quests");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Quests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Quests");

            migrationBuilder.AddColumn<string>(
                name: "StartingLocation",
                table: "Quests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartingLocationId",
                table: "Quests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
