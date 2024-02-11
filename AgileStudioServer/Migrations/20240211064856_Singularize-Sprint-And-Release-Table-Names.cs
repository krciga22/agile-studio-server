using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class SingularizeSprintAndReleaseTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_backlog_item_sprints_sprint_id",
                table: "backlog_item");

            migrationBuilder.DropForeignKey(
                name: "fk_releases_project_project_id",
                table: "releases");

            migrationBuilder.DropForeignKey(
                name: "fk_sprints_project_project_id",
                table: "sprints");

            migrationBuilder.DropPrimaryKey(
                name: "pk_sprints",
                table: "sprints");

            migrationBuilder.DropPrimaryKey(
                name: "pk_releases",
                table: "releases");

            migrationBuilder.RenameTable(
                name: "sprints",
                newName: "sprint");

            migrationBuilder.RenameTable(
                name: "releases",
                newName: "release");

            migrationBuilder.RenameIndex(
                name: "ix_sprints_project_id",
                table: "sprint",
                newName: "ix_sprint_project_id");

            migrationBuilder.RenameIndex(
                name: "ix_releases_project_id",
                table: "release",
                newName: "ix_release_project_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_sprint",
                table: "sprint",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_release",
                table: "release",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_backlog_item_sprint_sprint_id",
                table: "backlog_item",
                column: "sprint_id",
                principalTable: "sprint",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_release_project_project_id",
                table: "release",
                column: "project_id",
                principalTable: "project",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_sprint_project_project_id",
                table: "sprint",
                column: "project_id",
                principalTable: "project",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_backlog_item_sprint_sprint_id",
                table: "backlog_item");

            migrationBuilder.DropForeignKey(
                name: "fk_release_project_project_id",
                table: "release");

            migrationBuilder.DropForeignKey(
                name: "fk_sprint_project_project_id",
                table: "sprint");

            migrationBuilder.DropPrimaryKey(
                name: "pk_sprint",
                table: "sprint");

            migrationBuilder.DropPrimaryKey(
                name: "pk_release",
                table: "release");

            migrationBuilder.RenameTable(
                name: "sprint",
                newName: "sprints");

            migrationBuilder.RenameTable(
                name: "release",
                newName: "releases");

            migrationBuilder.RenameIndex(
                name: "ix_sprint_project_id",
                table: "sprints",
                newName: "ix_sprints_project_id");

            migrationBuilder.RenameIndex(
                name: "ix_release_project_id",
                table: "releases",
                newName: "ix_releases_project_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_sprints",
                table: "sprints",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_releases",
                table: "releases",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_backlog_item_sprints_sprint_id",
                table: "backlog_item",
                column: "sprint_id",
                principalTable: "sprints",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_releases_project_project_id",
                table: "releases",
                column: "project_id",
                principalTable: "project",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_sprints_project_project_id",
                table: "sprints",
                column: "project_id",
                principalTable: "project",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
