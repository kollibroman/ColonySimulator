using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColonySimulator.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddActivityStatusToThreat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "PlagueThreats",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "NaturalThreats",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "FightingThreats",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "PlagueThreats");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "NaturalThreats");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "FightingThreats");
        }
    }
}
