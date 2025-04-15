using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Finance.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedUserIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Payments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceUserId",
                table: "Incomes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerUserId",
                table: "Goals",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int[]>(
                name: "UserIds",
                table: "Boards",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "SourceUserId",
                table: "Incomes");

            migrationBuilder.DropColumn(
                name: "OwnerUserId",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "UserIds",
                table: "Boards");
        }
    }
}
