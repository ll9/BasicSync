using Microsoft.EntityFrameworkCore.Migrations;

namespace BasicSync.Migrations
{
    public partial class ConstraintFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BasicEntities",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Ort = table.Column<string>(nullable: true),
                    Straße = table.Column<string>(nullable: true),
                    Hausnummer = table.Column<int>(nullable: true),
                    SyncStatus = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicEntities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasicEntities");
        }
    }
}
