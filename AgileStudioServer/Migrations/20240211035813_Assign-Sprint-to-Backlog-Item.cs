using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AssignSprinttoBacklogItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sprint_id",
                table: "backlog_item",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_sprint_id",
                table: "backlog_item",
                column: "sprint_id");

            migrationBuilder.AddForeignKey(
                name: "fk_backlog_item_sprints_sprint_id",
                table: "backlog_item",
                column: "sprint_id",
                principalTable: "sprints",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_backlog_item_sprints_sprint_id",
                table: "backlog_item");

            migrationBuilder.DropIndex(
                name: "ix_backlog_item_sprint_id",
                table: "backlog_item");

            migrationBuilder.DropColumn(
                name: "sprint_id",
                table: "backlog_item");
        }
    }
}
