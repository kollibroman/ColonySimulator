using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColonySimulator.Backend.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Apothecaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ApothecaryLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    Vitality = table.Column<double>(type: "REAL", nullable: false),
                    Strength = table.Column<double>(type: "REAL", nullable: false),
                    Agility = table.Column<double>(type: "REAL", nullable: false),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false),
                    ResourceConsumption = table.Column<double>(type: "REAL", nullable: false),
                    RequiredStrength = table.Column<double>(type: "REAL", nullable: false),
                    RequiredAgility = table.Column<double>(type: "REAL", nullable: false)
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
                    BlackSmithLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    Vitality = table.Column<double>(type: "REAL", nullable: false),
                    Strength = table.Column<double>(type: "REAL", nullable: false),
                    Agility = table.Column<double>(type: "REAL", nullable: false),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false),
                    ResourceConsumption = table.Column<double>(type: "REAL", nullable: false),
                    RequiredStrength = table.Column<double>(type: "REAL", nullable: false),
                    RequiredAgility = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackSmiths", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Crops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CropsCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Farmers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FarmingLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    Vitality = table.Column<double>(type: "REAL", nullable: false),
                    Strength = table.Column<double>(type: "REAL", nullable: false),
                    Agility = table.Column<double>(type: "REAL", nullable: false),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false),
                    ResourceConsumption = table.Column<double>(type: "REAL", nullable: false),
                    RequiredStrength = table.Column<double>(type: "REAL", nullable: false),
                    RequiredAgility = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farmers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FightingThreats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RequiredSmithingLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    RequiredWeaponryCount = table.Column<int>(type: "INTEGER", nullable: false),
                    ThreatLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FightingThreats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MedicineCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MedicLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    Vitality = table.Column<double>(type: "REAL", nullable: false),
                    Strength = table.Column<double>(type: "REAL", nullable: false),
                    Agility = table.Column<double>(type: "REAL", nullable: false),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false),
                    ResourceConsumption = table.Column<double>(type: "REAL", nullable: false),
                    RequiredStrength = table.Column<double>(type: "REAL", nullable: false),
                    RequiredAgility = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NaturalThreats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RequiredFarmingLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    RequiredCropsCount = table.Column<int>(type: "INTEGER", nullable: false),
                    ThreatLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NaturalThreats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlagueThreats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RequiredMedicalLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    RequiredMedicineCount = table.Column<int>(type: "INTEGER", nullable: false),
                    ThreatLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlagueThreats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Timbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TimberLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    Vitality = table.Column<double>(type: "REAL", nullable: false),
                    Strength = table.Column<double>(type: "REAL", nullable: false),
                    Agility = table.Column<double>(type: "REAL", nullable: false),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false),
                    ResourceConsumption = table.Column<double>(type: "REAL", nullable: false),
                    RequiredStrength = table.Column<double>(type: "REAL", nullable: false),
                    RequiredAgility = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timbers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Traders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TradingLevel = table.Column<int>(type: "INTEGER", nullable: false),
                    Vitality = table.Column<double>(type: "REAL", nullable: false),
                    Strength = table.Column<double>(type: "REAL", nullable: false),
                    Agility = table.Column<double>(type: "REAL", nullable: false),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false),
                    ResourceConsumption = table.Column<double>(type: "REAL", nullable: false),
                    RequiredStrength = table.Column<double>(type: "REAL", nullable: false),
                    RequiredAgility = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Weaponry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WeaponryCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weaponry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wood",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    WoodCount = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wood", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apothecaries");

            migrationBuilder.DropTable(
                name: "BlackSmiths");

            migrationBuilder.DropTable(
                name: "Crops");

            migrationBuilder.DropTable(
                name: "Farmers");

            migrationBuilder.DropTable(
                name: "FightingThreats");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "Medics");

            migrationBuilder.DropTable(
                name: "NaturalThreats");

            migrationBuilder.DropTable(
                name: "PlagueThreats");

            migrationBuilder.DropTable(
                name: "Timbers");

            migrationBuilder.DropTable(
                name: "Traders");

            migrationBuilder.DropTable(
                name: "Weaponry");

            migrationBuilder.DropTable(
                name: "Wood");
        }
    }
}
