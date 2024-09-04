using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace haveaseatapi.Migrations
{
    /// <inheritdoc />
    public partial class BorderMaxLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Border",
                table: "Cells",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(23)",
                oldMaxLength: 23);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Border",
                table: "Cells",
                type: "character varying(23)",
                maxLength: 23,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);
        }
    }
}
