using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class ticketupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Employees_assignedEmployeeemployeeId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Employees_reporteremployeeId",
                table: "Ticket");


            migrationBuilder.AlterColumn<int>(
                name: "reporteremployeeId",
                table: "Ticket",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "assignedEmployeeemployeeId",
                table: "Ticket",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Employees_assignedEmployeeemployeeId",
                table: "Ticket",
                column: "assignedEmployeeemployeeId",
                principalTable: "Employees",
                principalColumn: "employeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Employees_reporteremployeeId",
                table: "Ticket",
                column: "reporteremployeeId",
                principalTable: "Employees",
                principalColumn: "employeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Sprint_sprintId",
                table: "Ticket",
                column: "sprintId",
                principalTable: "Sprint",
                principalColumn: "sprintId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Employees_assignedEmployeeemployeeId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Employees_reporteremployeeId",
                table: "Ticket");

            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Sprint_sprintId",
                table: "Ticket");

            migrationBuilder.AlterColumn<int>(
                name: "sprintId",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "reporteremployeeId",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "assignedEmployeeemployeeId",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Employees_assignedEmployeeemployeeId",
                table: "Ticket",
                column: "assignedEmployeeemployeeId",
                principalTable: "Employees",
                principalColumn: "employeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Employees_reporteremployeeId",
                table: "Ticket",
                column: "reporteremployeeId",
                principalTable: "Employees",
                principalColumn: "employeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Sprint_sprintId",
                table: "Ticket",
                column: "sprintId",
                principalTable: "Sprint",
                principalColumn: "sprintId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
