using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class SingularizeProjectAndBacklogItemSchemaType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_backlog_item_type_backlog_item_type_schema_id",
                table: "backlog_item_type");

            migrationBuilder.DropForeignKey(
                name: "fk_backlog_item_type_schemas_projects_project_id",
                table: "backlog_item_type_schemas");

            migrationBuilder.DropPrimaryKey(
                name: "pk_projects",
                table: "projects");

            migrationBuilder.DropPrimaryKey(
                name: "pk_backlog_item_type_schemas",
                table: "backlog_item_type_schemas");

            migrationBuilder.RenameTable(
                name: "projects",
                newName: "project");

            migrationBuilder.RenameTable(
                name: "backlog_item_type_schemas",
                newName: "backlog_item_type_schema");

            migrationBuilder.RenameIndex(
                name: "ix_backlog_item_type_schemas_project_id",
                table: "backlog_item_type_schema",
                newName: "ix_backlog_item_type_schema_project_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_project",
                table: "project",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_backlog_item_type_schema",
                table: "backlog_item_type_schema",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_backlog_item_type_backlog_item_type_schema_id",
                table: "backlog_item_type",
                column: "backlog_item_type_schema_id",
                principalTable: "backlog_item_type_schema",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_backlog_item_type_schema_project_project_id",
                table: "backlog_item_type_schema",
                column: "project_id",
                principalTable: "project",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_backlog_item_type_backlog_item_type_schema_id",
                table: "backlog_item_type");

            migrationBuilder.DropForeignKey(
                name: "fk_backlog_item_type_schema_project_project_id",
                table: "backlog_item_type_schema");

            migrationBuilder.DropPrimaryKey(
                name: "pk_project",
                table: "project");

            migrationBuilder.DropPrimaryKey(
                name: "pk_backlog_item_type_schema",
                table: "backlog_item_type_schema");

            migrationBuilder.RenameTable(
                name: "project",
                newName: "projects");

            migrationBuilder.RenameTable(
                name: "backlog_item_type_schema",
                newName: "backlog_item_type_schemas");

            migrationBuilder.RenameIndex(
                name: "ix_backlog_item_type_schema_project_id",
                table: "backlog_item_type_schemas",
                newName: "ix_backlog_item_type_schemas_project_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_projects",
                table: "projects",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_backlog_item_type_schemas",
                table: "backlog_item_type_schemas",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_backlog_item_type_backlog_item_type_schema_id",
                table: "backlog_item_type",
                column: "backlog_item_type_schema_id",
                principalTable: "backlog_item_type_schemas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_backlog_item_type_schemas_projects_project_id",
                table: "backlog_item_type_schemas",
                column: "project_id",
                principalTable: "projects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
