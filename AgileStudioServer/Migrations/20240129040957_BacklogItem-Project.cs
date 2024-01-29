using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class BacklogItemProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "project_id",
                table: "backlog_item",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_project_id",
                table: "backlog_item",
                column: "project_id");

            migrationBuilder.AddForeignKey(
                name: "fk_backlog_item_project_project_id",
                table: "backlog_item",
                column: "project_id",
                principalTable: "project",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_backlog_item_project_project_id",
                table: "backlog_item");

            migrationBuilder.DropIndex(
                name: "ix_backlog_item_project_id",
                table: "backlog_item");

            migrationBuilder.DropColumn(
                name: "project_id",
                table: "backlog_item");
        }
    }
}
