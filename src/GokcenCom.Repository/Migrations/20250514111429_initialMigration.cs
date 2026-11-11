using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GokcenCom.Repository.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UAVCurrentLocations",
                columns: table => new
                {
                    takim_numarasi = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    iha_enlem = table.Column<double>(type: "double precision", nullable: false),
                    iha_boylam = table.Column<double>(type: "double precision", nullable: false),
                    iha_irtifa = table.Column<double>(type: "double precision", nullable: false),
                    iha_dikilme = table.Column<double>(type: "double precision", nullable: false),
                    iha_yonelme = table.Column<int>(type: "integer", nullable: false),
                    iha_yatis = table.Column<double>(type: "double precision", nullable: false),
                    iha_hizi = table.Column<double>(type: "double precision", nullable: false),
                    zaman_farki = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UAVCurrentLocations", x => x.takim_numarasi);
                });

            migrationBuilder.CreateTable(
                name: "UAVLocationHistorys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    takim_numarasi = table.Column<int>(type: "integer", nullable: false),
                    iha_enlem = table.Column<double>(type: "double precision", nullable: false),
                    iha_boylam = table.Column<double>(type: "double precision", nullable: false),
                    iha_irtifa = table.Column<double>(type: "double precision", nullable: false),
                    iha_dikilme = table.Column<double>(type: "double precision", nullable: false),
                    iha_yonelme = table.Column<int>(type: "integer", nullable: false),
                    iha_yatis = table.Column<double>(type: "double precision", nullable: false),
                    iha_hizi = table.Column<double>(type: "double precision", nullable: false),
                    zaman_farki = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UAVLocationHistorys", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UAVCurrentLocations");

            migrationBuilder.DropTable(
                name: "UAVLocationHistorys");
        }
    }
}
