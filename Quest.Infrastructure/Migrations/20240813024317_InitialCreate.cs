using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayerQuestState",
                columns: table => new
                {
                    PlayerId = table.Column<string>(type: "TEXT", nullable: false),
                    TotalQuestPercentCompleted = table.Column<float>(type: "REAL", nullable: false),
                    LastMilestoneIndexCompleted = table.Column<int>(type: "INTEGER", nullable: false),
                    QuestPointsEarned = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerQuestState", x => x.PlayerId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerQuestState");
        }
    }
}
