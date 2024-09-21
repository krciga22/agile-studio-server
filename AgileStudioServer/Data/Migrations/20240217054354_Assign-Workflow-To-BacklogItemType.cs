using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AssignWorkflowToBacklogItemType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "workflow_id",
                table: "backlog_item_type",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_type_workflow_id",
                table: "backlog_item_type",
                column: "workflow_id");

            migrationBuilder.AddForeignKey(
                name: "fk_backlog_item_type_workflow_workflow_id",
                table: "backlog_item_type",
                column: "workflow_id",
                principalTable: "workflow",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_backlog_item_type_workflow_workflow_id",
                table: "backlog_item_type");

            migrationBuilder.DropIndex(
                name: "ix_backlog_item_type_workflow_id",
                table: "backlog_item_type");

            migrationBuilder.DropColumn(
                name: "workflow_id",
                table: "backlog_item_type");
        }
    }
}
