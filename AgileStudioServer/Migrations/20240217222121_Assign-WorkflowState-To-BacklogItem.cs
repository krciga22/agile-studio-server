using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AssignWorkflowStateToBacklogItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "workflow_state_id",
                table: "backlog_item",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_workflow_state_id",
                table: "backlog_item",
                column: "workflow_state_id");

            migrationBuilder.AddForeignKey(
                name: "fk_backlog_item_workflow_state_id",
                table: "backlog_item",
                column: "workflow_state_id",
                principalTable: "workflow_state",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_backlog_item_workflow_state_id",
                table: "backlog_item");

            migrationBuilder.DropIndex(
                name: "ix_backlog_item_workflow_state_id",
                table: "backlog_item");

            migrationBuilder.DropColumn(
                name: "workflow_state_id",
                table: "backlog_item");
        }
    }
}
