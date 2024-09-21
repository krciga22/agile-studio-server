using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AssignCreatedByUsertoOtherEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "created_by_id",
                table: "workflow_state",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "created_by_id",
                table: "workflow",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "created_by_id",
                table: "sprint",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "created_by_id",
                table: "release",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "created_by_id",
                table: "backlog_item_type_schema",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "created_by_id",
                table: "backlog_item_type",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "created_by_id",
                table: "backlog_item",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_workflow_state_created_by_id",
                table: "workflow_state",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_workflow_created_by_id",
                table: "workflow",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_sprint_created_by_id",
                table: "sprint",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_release_created_by_id",
                table: "release",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_type_schema_created_by_id",
                table: "backlog_item_type_schema",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_type_created_by_id",
                table: "backlog_item_type",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_created_by_id",
                table: "backlog_item",
                column: "created_by_id");

            migrationBuilder.AddForeignKey(
                name: "fk_backlog_item_user_created_by_id",
                table: "backlog_item",
                column: "created_by_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_backlog_item_type_user_created_by_id",
                table: "backlog_item_type",
                column: "created_by_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_backlog_item_type_schema_user_created_by_id",
                table: "backlog_item_type_schema",
                column: "created_by_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_release_user_created_by_id",
                table: "release",
                column: "created_by_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_sprint_user_created_by_id",
                table: "sprint",
                column: "created_by_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_workflow_user_created_by_id",
                table: "workflow",
                column: "created_by_id",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_workflow_state_user_created_by_id",
                table: "workflow_state",
                column: "created_by_id",
                principalTable: "user",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_backlog_item_user_created_by_id",
                table: "backlog_item");

            migrationBuilder.DropForeignKey(
                name: "fk_backlog_item_type_user_created_by_id",
                table: "backlog_item_type");

            migrationBuilder.DropForeignKey(
                name: "fk_backlog_item_type_schema_user_created_by_id",
                table: "backlog_item_type_schema");

            migrationBuilder.DropForeignKey(
                name: "fk_release_user_created_by_id",
                table: "release");

            migrationBuilder.DropForeignKey(
                name: "fk_sprint_user_created_by_id",
                table: "sprint");

            migrationBuilder.DropForeignKey(
                name: "fk_workflow_user_created_by_id",
                table: "workflow");

            migrationBuilder.DropForeignKey(
                name: "fk_workflow_state_user_created_by_id",
                table: "workflow_state");

            migrationBuilder.DropIndex(
                name: "ix_workflow_state_created_by_id",
                table: "workflow_state");

            migrationBuilder.DropIndex(
                name: "ix_workflow_created_by_id",
                table: "workflow");

            migrationBuilder.DropIndex(
                name: "ix_sprint_created_by_id",
                table: "sprint");

            migrationBuilder.DropIndex(
                name: "ix_release_created_by_id",
                table: "release");

            migrationBuilder.DropIndex(
                name: "ix_backlog_item_type_schema_created_by_id",
                table: "backlog_item_type_schema");

            migrationBuilder.DropIndex(
                name: "ix_backlog_item_type_created_by_id",
                table: "backlog_item_type");

            migrationBuilder.DropIndex(
                name: "ix_backlog_item_created_by_id",
                table: "backlog_item");

            migrationBuilder.DropColumn(
                name: "created_by_id",
                table: "workflow_state");

            migrationBuilder.DropColumn(
                name: "created_by_id",
                table: "workflow");

            migrationBuilder.DropColumn(
                name: "created_by_id",
                table: "sprint");

            migrationBuilder.DropColumn(
                name: "created_by_id",
                table: "release");

            migrationBuilder.DropColumn(
                name: "created_by_id",
                table: "backlog_item_type_schema");

            migrationBuilder.DropColumn(
                name: "created_by_id",
                table: "backlog_item_type");

            migrationBuilder.DropColumn(
                name: "created_by_id",
                table: "backlog_item");
        }
    }
}
