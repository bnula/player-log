using Microsoft.EntityFrameworkCore.Migrations;

namespace player_log.Data.Migrations
{
    public partial class AddedCharacterVMTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CharacterCreateVM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Race = table.Column<string>(nullable: false),
                    MainClass = table.Column<string>(nullable: false),
                    Strength = table.Column<int>(nullable: false),
                    Dexterity = table.Column<int>(nullable: false),
                    Constitution = table.Column<int>(nullable: false),
                    Wisdom = table.Column<int>(nullable: false),
                    Intelligence = table.Column<int>(nullable: false),
                    Charisma = table.Column<int>(nullable: false),
                    Campaign = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterCreateVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterDetailsVM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Race = table.Column<string>(nullable: true),
                    MainClass = table.Column<string>(nullable: true),
                    Multiclass = table.Column<bool>(nullable: false),
                    SecondClass = table.Column<string>(nullable: true),
                    ThirdClass = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    Strength = table.Column<int>(nullable: false),
                    Dexterity = table.Column<int>(nullable: false),
                    Constitution = table.Column<int>(nullable: false),
                    Wisdom = table.Column<int>(nullable: false),
                    Intelligence = table.Column<int>(nullable: false),
                    Charisma = table.Column<int>(nullable: false),
                    Campaign = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterDetailsVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterEditVM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Race = table.Column<string>(nullable: false),
                    MainClass = table.Column<string>(nullable: false),
                    Multiclass = table.Column<bool>(nullable: false),
                    SecondClass = table.Column<string>(nullable: true),
                    ThirdClass = table.Column<string>(nullable: true),
                    Strength = table.Column<int>(nullable: false),
                    Dexterity = table.Column<int>(nullable: false),
                    Constitution = table.Column<int>(nullable: false),
                    Wisdom = table.Column<int>(nullable: false),
                    Intelligence = table.Column<int>(nullable: false),
                    Charisma = table.Column<int>(nullable: false),
                    Campaign = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterEditVM", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterCreateVM");

            migrationBuilder.DropTable(
                name: "CharacterDetailsVM");

            migrationBuilder.DropTable(
                name: "CharacterEditVM");
        }
    }
}
