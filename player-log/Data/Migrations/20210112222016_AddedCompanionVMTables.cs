using Microsoft.EntityFrameworkCore.Migrations;

namespace player_log.Data.Migrations
{
    public partial class AddedCompanionVMTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Companions",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "CompanionCreateVM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Cr = table.Column<int>(nullable: false),
                    CompanionClass = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Campaign = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanionCreateVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanionDetailsVM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Cr = table.Column<int>(nullable: false),
                    CompanionClass = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Campaign = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanionDetailsVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanionEditVM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Cr = table.Column<int>(nullable: false),
                    CompanionClass = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Campaign = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanionEditVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanionListVM",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Campaign = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanionListVM", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanionCreateVM");

            migrationBuilder.DropTable(
                name: "CompanionDetailsVM");

            migrationBuilder.DropTable(
                name: "CompanionEditVM");

            migrationBuilder.DropTable(
                name: "CompanionListVM");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Companions",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
