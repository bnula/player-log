using Microsoft.EntityFrameworkCore.Migrations;

namespace player_log.Data.Migrations
{
    public partial class AddedNotesPropertyToCampaignAndCampaignVMs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Campaigns",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "CampaignDetailsVM",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "CampaignDetailsVM",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "CampaignDetailsVM",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "CampaignDetailsVM");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "CampaignDetailsVM");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "CampaignDetailsVM");
        }
    }
}
