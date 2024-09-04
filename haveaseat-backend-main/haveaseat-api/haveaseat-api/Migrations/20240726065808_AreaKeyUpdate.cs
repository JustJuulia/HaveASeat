using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace haveaseatapi.Migrations
{
    /// <inheritdoc />
    public partial class AreaKeyUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "width",
                table: "Area",
                newName: "Width");

            migrationBuilder.RenameColumn(
                name: "height",
                table: "Area",
                newName: "Height");

            migrationBuilder.AddColumn<short>(
                name: "Id",
                table: "Area",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Area",
                table: "Area",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Area",
                columns: new[] { "Id", "Height", "Width" },
                values: new object[] { (short)1, 11, 15 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Area",
                table: "Area");

            migrationBuilder.DeleteData(
                table: "Area",
                keyColumn: "Id",
                keyColumnType: "smallint",
                keyValue: (short)1);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Area");

            migrationBuilder.RenameColumn(
                name: "Width",
                table: "Area",
                newName: "width");

            migrationBuilder.RenameColumn(
                name: "Height",
                table: "Area",
                newName: "height");
        }
    }
}
