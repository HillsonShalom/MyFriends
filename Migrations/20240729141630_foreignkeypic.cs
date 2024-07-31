using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFriends.Migrations
{
    /// <inheritdoc />
    public partial class foreignkeypic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Friends_ProfileId",
                table: "Friends");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_FriendId",
                table: "Pictures",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_ProfileId",
                table: "Friends",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Friends_FriendId",
                table: "Pictures",
                column: "FriendId",
                principalTable: "Friends",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Friends_FriendId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_FriendId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Friends_ProfileId",
                table: "Friends");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_ProfileId",
                table: "Friends",
                column: "ProfileId",
                unique: true,
                filter: "[ProfileId] IS NOT NULL");
        }
    }
}
