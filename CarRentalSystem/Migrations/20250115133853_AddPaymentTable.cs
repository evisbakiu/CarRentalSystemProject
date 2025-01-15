using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalSystem.Migrations
{
    public partial class AddPaymentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Categories_CategoryId1",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_AspNetUsers_UserId1",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_UserId1",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Cars_CarId1",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Statuses_StatusId1",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reports",
                table: "Reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cars",
                table: "Cars");

            migrationBuilder.RenameTable(
                name: "Statuses",
                newName: "Status");

            migrationBuilder.RenameTable(
                name: "Reservations",
                newName: "Reservation");

            migrationBuilder.RenameTable(
                name: "Reports",
                newName: "Report");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "Cars",
                newName: "Car");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_UserId1",
                table: "Reservation",
                newName: "IX_Reservation_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_StatusId1",
                table: "Reservation",
                newName: "IX_Reservation_StatusId1");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_CarId1",
                table: "Reservation",
                newName: "IX_Reservation_CarId1");

            migrationBuilder.RenameIndex(
                name: "IX_Reports_UserId1",
                table: "Report",
                newName: "IX_Report_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_CategoryId1",
                table: "Car",
                newName: "IX_Car_CategoryId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Status",
                table: "Status",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Report",
                table: "Report",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Car",
                table: "Car",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    ReservationId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Reservation_ReservationId1",
                        column: x => x.ReservationId1,
                        principalTable: "Reservation",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_ReservationId1",
                table: "Payment",
                column: "ReservationId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Category_CategoryId1",
                table: "Car",
                column: "CategoryId1",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_AspNetUsers_UserId1",
                table: "Report",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_AspNetUsers_UserId1",
                table: "Reservation",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Car_CarId1",
                table: "Reservation",
                column: "CarId1",
                principalTable: "Car",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Status_StatusId1",
                table: "Reservation",
                column: "StatusId1",
                principalTable: "Status",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Category_CategoryId1",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_AspNetUsers_UserId1",
                table: "Report");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_AspNetUsers_UserId1",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Car_CarId1",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Status_StatusId1",
                table: "Reservation");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Status",
                table: "Status");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Report",
                table: "Report");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Car",
                table: "Car");

            migrationBuilder.RenameTable(
                name: "Status",
                newName: "Statuses");

            migrationBuilder.RenameTable(
                name: "Reservation",
                newName: "Reservations");

            migrationBuilder.RenameTable(
                name: "Report",
                newName: "Reports");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameTable(
                name: "Car",
                newName: "Cars");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_UserId1",
                table: "Reservations",
                newName: "IX_Reservations_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_StatusId1",
                table: "Reservations",
                newName: "IX_Reservations_StatusId1");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_CarId1",
                table: "Reservations",
                newName: "IX_Reservations_CarId1");

            migrationBuilder.RenameIndex(
                name: "IX_Report_UserId1",
                table: "Reports",
                newName: "IX_Reports_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Car_CategoryId1",
                table: "Cars",
                newName: "IX_Cars_CategoryId1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reports",
                table: "Reports",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cars",
                table: "Cars",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Categories_CategoryId1",
                table: "Cars",
                column: "CategoryId1",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_AspNetUsers_UserId1",
                table: "Reports",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_UserId1",
                table: "Reservations",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Cars_CarId1",
                table: "Reservations",
                column: "CarId1",
                principalTable: "Cars",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Statuses_StatusId1",
                table: "Reservations",
                column: "StatusId1",
                principalTable: "Statuses",
                principalColumn: "Id");
        }
    }
}
