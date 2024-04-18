using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SkeletonApi.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class db_hino3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "DeviceData",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "DeviceData",
                newName: "time");

            migrationBuilder.RenameColumn(
                name: "Quality",
                table: "DeviceData",
                newName: "quality");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DeviceData",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "DeviceData",
                newName: "date_time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "value",
                table: "DeviceData",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "time",
                table: "DeviceData",
                newName: "Time");

            migrationBuilder.RenameColumn(
                name: "quality",
                table: "DeviceData",
                newName: "Quality");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "DeviceData",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "date_time",
                table: "DeviceData",
                newName: "DateTime");
        }
    }
}
