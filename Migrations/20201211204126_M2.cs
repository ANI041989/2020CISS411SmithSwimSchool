using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project1.Migrations
{
    public partial class M2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "DailyStartTime",
                table: "Sessions",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Sessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Sessions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "Coachs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coachs_LessonId",
                table: "Coachs",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coachs_Lessons_LessonId",
                table: "Coachs",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "LessonId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coachs_Lessons_LessonId",
                table: "Coachs");

            migrationBuilder.DropIndex(
                name: "IX_Coachs_LessonId",
                table: "Coachs");

            migrationBuilder.DropColumn(
                name: "DailyStartTime",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "Coachs");
        }
    }
}
