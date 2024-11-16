using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class ParentBacklogItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "parent_backlog_item_id",
                table: "backlog_item",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_parent_backlog_item_id",
                table: "backlog_item",
                column: "parent_backlog_item_id");

            migrationBuilder.AddForeignKey(
                name: "fk_backlog_item_parent_backlog_item_id",
                table: "backlog_item",
                column: "parent_backlog_item_id",
                principalTable: "backlog_item",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_backlog_item_parent_backlog_item_id",
                table: "backlog_item");

            migrationBuilder.DropIndex(
                name: "ix_backlog_item_parent_backlog_item_id",
                table: "backlog_item");

            migrationBuilder.DropColumn(
                name: "parent_backlog_item_id",
                table: "backlog_item");
        }
    }
}
