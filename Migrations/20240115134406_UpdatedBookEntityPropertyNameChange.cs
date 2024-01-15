using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedBookEntityPropertyNameChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableCopies",
                table: "BorrowedBook");

            migrationBuilder.RenameColumn(
                name: "AvailableCopies",
                table: "Books",
                newName: "Copies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Copies",
                table: "Books",
                newName: "AvailableCopies");

            migrationBuilder.AddColumn<int>(
                name: "AvailableCopies",
                table: "BorrowedBook",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
