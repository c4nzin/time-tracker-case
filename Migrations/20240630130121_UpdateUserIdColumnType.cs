using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace time_tracker_case.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserIdColumnType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Projects",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Projects",
                type: "text",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid"
            );
        }
    }
}
