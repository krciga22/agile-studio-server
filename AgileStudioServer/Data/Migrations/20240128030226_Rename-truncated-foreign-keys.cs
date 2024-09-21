using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class Renametruncatedforeignkeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_backlog_item_backlog_item_type_backlog_item_type_id", 
                table: "backlog_item"
            );
            migrationBuilder.AddForeignKey(
                name: "fk_backlog_item_backlog_item_type_id",
                table: "backlog_item",
                column: "backlog_item_type_id",
                principalTable: "backlog_item_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.DropForeignKey("fk_backlog_item_type_backlog_item_type_schemas_backlog_item_type", "backlog_item_type");
            migrationBuilder.AddForeignKey(
                name: "fk_backlog_item_type_backlog_item_type_schema_id",
                table: "backlog_item_type",
                column: "backlog_item_type_schema_id",
                principalTable: "backlog_item_type_schemas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_backlog_item_backlog_item_type_id",
                table: "backlog_item"
            );
            migrationBuilder.AddForeignKey(
                name: "fk_backlog_item_backlog_item_type_backlog_item_type_id",
                table: "backlog_item",
                column: "backlog_item_type_id",
                principalTable: "backlog_item_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.DropForeignKey("fk_backlog_item_type_backlog_item_type_schema_id", "backlog_item_type");
            migrationBuilder.AddForeignKey(
                name: "fk_backlog_item_type_backlog_item_type_schemas_backlog_item_type",
                table: "backlog_item_type",
                column: "backlog_item_type_schema_id",
                principalTable: "backlog_item_type_schemas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}
