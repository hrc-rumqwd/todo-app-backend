using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApp.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AdjustActivableToRemovable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActivable",
                table: "TaskItems",
                newName: "IsRemove");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TaskItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TaskItems");

            migrationBuilder.RenameColumn(
                name: "IsRemove",
                table: "TaskItems",
                newName: "IsActivable");
        }
    }
}
