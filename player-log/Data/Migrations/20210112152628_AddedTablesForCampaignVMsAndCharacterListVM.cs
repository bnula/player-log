using Microsoft.EntityFrameworkCore.Migrations;

namespace player_log.Data.Migrations
{
    public partial class AddedTablesForCampaignVMsAndCharacterListVM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CampaignCreateVM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignCreateVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CampaignDetailsVM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignDetailsVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CampaignEditVM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignEditVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CampaignListVM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignListVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterListVM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Race = table.Column<string>(nullable: true),
                    MainClass = table.Column<string>(nullable: true),
                    Multiclass = table.Column<bool>(nullable: false),
                    Campaign = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterListVM", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampaignCreateVM");

            migrationBuilder.DropTable(
                name: "CampaignDetailsVM");

            migrationBuilder.DropTable(
                name: "CampaignEditVM");

            migrationBuilder.DropTable(
                name: "CampaignListVM");

            migrationBuilder.DropTable(
                name: "CharacterListVM");
        }
    }
}
