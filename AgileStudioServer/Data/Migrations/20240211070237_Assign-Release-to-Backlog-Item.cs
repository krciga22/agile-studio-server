using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AssignReleasetoBacklogItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "release_id",
                table: "backlog_item",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_backlog_item_release_id",
                table: "backlog_item",
                column: "release_id");

            migrationBuilder.AddForeignKey(
                name: "fk_backlog_item_release_release_id",
                table: "backlog_item",
                column: "release_id",
                principalTable: "release",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_backlog_item_release_release_id",
                table: "backlog_item");

            migrationBuilder.DropIndex(
                name: "ix_backlog_item_release_id",
                table: "backlog_item");

            migrationBuilder.DropColumn(
                name: "release_id",
                table: "backlog_item");
        }
    }
}
