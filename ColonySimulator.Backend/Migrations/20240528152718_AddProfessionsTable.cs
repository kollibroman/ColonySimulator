using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColonySimulator.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddProfessionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apothecaries");

            migrationBuilder.DropTable(
                name: "BlackSmiths");

            migrationBuilder.DropTable(
                name: "Farmers");

            migrationBuilder.DropTable(
                name: "Medics");

            migrationBuilder.DropTable(
                name: "Timbers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Traders",
                table: "Traders");

            migrationBuilder.RenameTable(
                name: "Traders",
                newName: "Proffesions");

            migrationBuilder.AlterColumn<int>(
                name: "TradingLevel",
                table: "Proffesions",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "ApothecaryLevel",
                table: "Proffesions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BlackSmithLevel",
                table: "Proffesions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Proffesions",
                type: "TEXT",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "FarmingLevel",
                table: "Proffesions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicLevel",
                table: "Proffesions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TimberLevel",
                table: "Proffesions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Proffesions",
                table: "Proffesions",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Proffesions",
                table: "Proffesions");

            migrationBuilder.DropColumn(
                name: "ApothecaryLevel",
                table: "Proffesions");

            migrationBuilder.DropColumn(
                name: "BlackSmithLevel",
                table: "Proffesions");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Proffesions");

            migrationBuilder.DropColumn(
                name: "FarmingLevel",
                table: "Proffesions");

            migrationBuilder.DropColumn(
                name: "MedicLevel",
                table: "Proffesions");

            migrationBuilder.DropColumn(
                name: "TimberLevel",
                table: "Proffesions");

            migrationBuilder.RenameTable(
                name: "Proffesions",
                newName: "Traders");

            migrationBuilder.AlterColumn<int>(
                name: "TradingLevel",
                table: "Traders",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Traders",
                table: "Traders",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Apothecaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Agility = table.Column<int>(type: "INTEGER", nullable: false),
                    ApothecaryLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false),
                    IsSick = table.Column<bool>(type: "INTEGER", nullable: false),
                    RequiredAgility = table.Column<double>(type: "REAL", nullable: false),
                    RequiredStrength = table.Column<double>(type: "REAL", nullable: false),
                    ResourceConsumption = table.Column<int>(type: "INTEGER", nullable: false),
                    Strength = table.Column<int>(type: "INTEGER", nullable: false),
                    Vitality = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apothecaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlackSmiths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Agility = table.Column<int>(type: "INTEGER", nullable: false),
                    BlackSmithLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false),
                    IsSick = table.Column<bool>(type: "INTEGER", nullable: false),
                    RequiredAgility = table.Column<double>(type: "REAL", nullable: false),
                    RequiredStrength = table.Column<double>(type: "REAL", nullable: false),
                    ResourceConsumption = table.Column<int>(type: "INTEGER", nullable: false),
                    Strength = table.Column<int>(type: "INTEGER", nullable: false),
                    Vitality = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackSmiths", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Farmers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Agility = table.Column<int>(type: "INTEGER", nullable: false),
                    FarmingLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false),
                    IsSick = table.Column<bool>(type: "INTEGER", nullable: false),
                    RequiredAgility = table.Column<double>(type: "REAL", nullable: false),
                    RequiredStrength = table.Column<double>(type: "REAL", nullable: false),
                    ResourceConsumption = table.Column<int>(type: "INTEGER", nullable: false),
                    Strength = table.Column<int>(type: "INTEGER", nullable: false),
                    Vitality = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farmers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Agility = table.Column<int>(type: "INTEGER", nullable: false),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false),
                    IsSick = table.Column<bool>(type: "INTEGER", nullable: false),
                    MedicLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    RequiredAgility = table.Column<double>(type: "REAL", nullable: false),
                    RequiredStrength = table.Column<double>(type: "REAL", nullable: false),
                    ResourceConsumption = table.Column<int>(type: "INTEGER", nullable: false),
                    Strength = table.Column<int>(type: "INTEGER", nullable: false),
                    Vitality = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Timbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Agility = table.Column<int>(type: "INTEGER", nullable: false),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false),
                    IsSick = table.Column<bool>(type: "INTEGER", nullable: false),
                    RequiredAgility = table.Column<double>(type: "REAL", nullable: false),
                    RequiredStrength = table.Column<double>(type: "REAL", nullable: false),
                    ResourceConsumption = table.Column<int>(type: "INTEGER", nullable: false),
                    Strength = table.Column<int>(type: "INTEGER", nullable: false),
                    TimberLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    Vitality = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timbers", x => x.Id);
                });
        }
    }
}
