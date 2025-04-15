using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRUDforPostswithSyncCommand.Migrations
{
    /// <inheritdoc />
    public partial class AlterToPostTapleInExtenalId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Posts_ExternalId",
                table: "Posts",
                column: "ExternalId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posts_ExternalId",
                table: "Posts");
        }
    }
}
