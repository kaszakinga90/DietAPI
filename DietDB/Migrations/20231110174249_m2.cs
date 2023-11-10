using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DietDB.Migrations
{
    public partial class m2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealTimeToXYAxis_Diet_DietId",
                table: "MealTimeToXYAxis");

            migrationBuilder.AlterColumn<int>(
                name: "DietId",
                table: "MealTimeToXYAxis",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_MealTimeToXYAxis_Diet_DietId",
                table: "MealTimeToXYAxis",
                column: "DietId",
                principalTable: "Diet",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealTimeToXYAxis_Diet_DietId",
                table: "MealTimeToXYAxis");

            migrationBuilder.AlterColumn<int>(
                name: "DietId",
                table: "MealTimeToXYAxis",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MealTimeToXYAxis_Diet_DietId",
                table: "MealTimeToXYAxis",
                column: "DietId",
                principalTable: "Diet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
