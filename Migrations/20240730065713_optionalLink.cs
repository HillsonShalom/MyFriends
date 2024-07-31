using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFriends.Migrations
{
    /// <inheritdoc />
    public partial class optionalLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Friends_FriendId",
                table: "Pictures");

            migrationBuilder.AlterColumn<int>(
                name: "FriendId",
                table: "Pictures",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Friends_FriendId",
                table: "Pictures",
                column: "FriendId",
                principalTable: "Friends",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Friends_FriendId",
                table: "Pictures");

            migrationBuilder.AlterColumn<int>(
                name: "FriendId",
                table: "Pictures",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Friends_FriendId",
                table: "Pictures",
                column: "FriendId",
                principalTable: "Friends",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
