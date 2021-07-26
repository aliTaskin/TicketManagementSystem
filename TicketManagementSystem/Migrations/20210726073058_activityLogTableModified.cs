using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketManagementSystem.Migrations
{
    public partial class activityLogTableModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AssignerSurname",
                table: "ActivityLogs",
                newName: "AssignedToSurname");

            migrationBuilder.RenameColumn(
                name: "AssignerName",
                table: "ActivityLogs",
                newName: "AssignedToName");

            migrationBuilder.RenameColumn(
                name: "AssignedSurname",
                table: "ActivityLogs",
                newName: "ActionTakerSurname");

            migrationBuilder.RenameColumn(
                name: "AssignedName",
                table: "ActivityLogs",
                newName: "ActionTakerName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AssignedToSurname",
                table: "ActivityLogs",
                newName: "AssignerSurname");

            migrationBuilder.RenameColumn(
                name: "AssignedToName",
                table: "ActivityLogs",
                newName: "AssignerName");

            migrationBuilder.RenameColumn(
                name: "ActionTakerSurname",
                table: "ActivityLogs",
                newName: "AssignedSurname");

            migrationBuilder.RenameColumn(
                name: "ActionTakerName",
                table: "ActivityLogs",
                newName: "AssignedName");
        }
    }
}
