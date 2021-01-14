using Microsoft.EntityFrameworkCore.Migrations;

namespace player_log.Data.Migrations
{
    public partial class AddedQuestVMs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Campaign",
                table: "CharacterEditVM",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Campaign",
                table: "CharacterCreateVM",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "QuestDetailsVM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    MainObjective = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    Reward = table.Column<string>(nullable: true),
                    Campaign = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestDetailsVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestListVM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Campaign = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestListVM", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestDetailsVM");

            migrationBuilder.DropTable(
                name: "QuestListVM");

            migrationBuilder.AlterColumn<string>(
                name: "Campaign",
                table: "CharacterEditVM",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Campaign",
                table: "CharacterCreateVM",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
