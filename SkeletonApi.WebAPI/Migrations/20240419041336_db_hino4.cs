using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkeletonApi.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class db_hino4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Repairs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    frame_number = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    entry = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    finish = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    update_by = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_by = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repairs", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Repairs");
        }
    }
}
