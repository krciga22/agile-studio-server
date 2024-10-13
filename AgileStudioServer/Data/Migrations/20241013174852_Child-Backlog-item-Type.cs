using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileStudioServer.Migrations
{
    /// <inheritdoc />
    public partial class ChildBacklogitemType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "child_backlog_item_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    child_type_id = table.Column<int>(type: "int", nullable: false),
                    parent_type_id = table.Column<int>(type: "int", nullable: false),
                    schema_id = table.Column<int>(type: "int", nullable: false),
                    created_on = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    created_by_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_child_backlog_item_type", x => x.id);
                    table.ForeignKey(
                        name: "fk_child_backlog_item_type_child_type_backlog_item_type_id",
                        column: x => x.child_type_id,
                        principalTable: "backlog_item_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_child_backlog_item_type_parent_type_backlog_item_type_id",
                        column: x => x.parent_type_id,
                        principalTable: "backlog_item_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_child_backlog_item_type_schema_backlog_item_type_schema_id",
                        column: x => x.schema_id,
                        principalTable: "backlog_item_type_schema",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_child_backlog_item_type_user_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "user",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_child_backlog_item_type_child_type_id",
                table: "child_backlog_item_type",
                column: "child_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_child_backlog_item_type_created_by_id",
                table: "child_backlog_item_type",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_child_backlog_item_type_parent_type_id",
                table: "child_backlog_item_type",
                column: "parent_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_child_backlog_item_type_schema_id",
                table: "child_backlog_item_type",
                column: "schema_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "child_backlog_item_type");
        }
    }
}
