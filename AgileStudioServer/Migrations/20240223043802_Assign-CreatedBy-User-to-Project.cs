using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class AssignCreatedByUsertoProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "created_by_id",
                table: "project",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_project_created_by_id",
                table: "project",
                column: "created_by_id");

            migrationBuilder.AddForeignKey(
                name: "fk_project_user_created_by_id",
                table: "project",
                column: "created_by_id",
                principalTable: "user",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_project_user_created_by_id",
                table: "project");

            migrationBuilder.DropIndex(
                name: "ix_project_created_by_id",
                table: "project");

            migrationBuilder.DropColumn(
                name: "created_by_id",
                table: "project");
        }
    }
}
