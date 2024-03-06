using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.BookingService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookingPackage",
                columns: table => new
                {
                    BookingID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfileID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    BookingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServicePackageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppoinmentTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingPackage", x => x.BookingID);
                });

            migrationBuilder.CreateTable(
                name: "DoctorCalendar",
                columns: table => new
                {
                    CalenderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    DoctorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorCalendar", x => x.CalenderID);
                });

            migrationBuilder.CreateTable(
                name: "DoctorSchedule",
                columns: table => new
                {
                    ScheduleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CalendarID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSchedule", x => x.ScheduleID);
                    table.ForeignKey(
                        name: "PK_DoctorCalendar_One_To_Many_DoctorSchedule",
                        column: x => x.CalendarID,
                        principalTable: "DoctorCalendar",
                        principalColumn: "CalenderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingDoctor",
                columns: table => new
                {
                    BookingID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    DoctorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfileID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    BookingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingType = table.Column<int>(type: "int", nullable: false),
                    BookingStatus = table.Column<int>(type: "int", nullable: false),
                    RoomID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScheduleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDoctor", x => x.BookingID);
                    table.ForeignKey(
                        name: "PK_DoctorSchedule_One_To_One_BookingDoctor",
                        column: x => x.ScheduleID,
                        principalTable: "DoctorSchedule",
                        principalColumn: "ScheduleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingDoctor_ScheduleID",
                table: "BookingDoctor",
                column: "ScheduleID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedule_CalendarID",
                table: "DoctorSchedule",
                column: "CalendarID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingDoctor");

            migrationBuilder.DropTable(
                name: "BookingPackage");

            migrationBuilder.DropTable(
                name: "DoctorSchedule");

            migrationBuilder.DropTable(
                name: "DoctorCalendar");
        }
    }
}
