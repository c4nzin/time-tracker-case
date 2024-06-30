using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace time_tracker_case.Migrations
{
    /// <inheritdoc />
    public partial class ProjectModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
        CREATE TABLE IF NOT EXISTS ""Projects"" (
            ""Id"" uuid NOT NULL,
            ""Name"" text NOT NULL,
            ""UserId"" uuid NOT NULL,
            CONSTRAINT ""PK_Projects"" PRIMARY KEY (""Id"")
        );
        "
            );

            // Specify how to convert existing data in UserId column to uuid
            migrationBuilder.Sql(
                @"
        ALTER TABLE ""Projects""
        ALTER COLUMN ""UserId"" TYPE uuid
        USING ""UserId""::uuid
        "
            );

            // Add other migration operations here
            migrationBuilder.AddColumn<Guid>(
                name: "Project",
                table: "TimeRecords",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000")
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Projects");

            migrationBuilder.DropColumn(name: "Project", table: "TimeRecords");
        }
    }
}
