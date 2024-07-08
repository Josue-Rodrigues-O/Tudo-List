using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tudo_list.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingUserToTodoListItemToRelateWithUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TodoListItems_UserId",
                table: "TodoListItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoListItems_Users_UserId",
                table: "TodoListItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoListItems_Users_UserId",
                table: "TodoListItems");

            migrationBuilder.DropIndex(
                name: "IX_TodoListItems_UserId",
                table: "TodoListItems");
        }
    }
}
