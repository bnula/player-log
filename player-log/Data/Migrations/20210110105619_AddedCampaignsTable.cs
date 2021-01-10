using Microsoft.EntityFrameworkCore.Migrations;

namespace player_log.Data.Migrations
{
    public partial class AddedCampaignsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Campaign",
                table: "Quests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CampaignId",
                table: "Quests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StartingLocation",
                table: "Quests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartingLocationId",
                table: "Quests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Campaign",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CampaignId",
                table: "Locations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "LocationInventory",
                table: "Locations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Campaign",
                table: "Companions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CampaignId",
                table: "Companions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Companions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Companions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Race",
                table: "Characters",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Characters",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MainClass",
                table: "Characters",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Campaign",
                table: "Characters",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CampaignId",
                table: "Characters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.DropColumn(
                name: "Campaign",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "StartingLocation",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "StartingLocationId",
                table: "Quests");

            migrationBuilder.DropColumn(
                name: "Campaign",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "LocationInventory",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Campaign",
                table: "Companions");

            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "Companions");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Companions");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Companions");

            migrationBuilder.DropColumn(
                name: "Campaign",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "Characters");

            migrationBuilder.AlterColumn<string>(
                name: "Race",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MainClass",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
