using Microsoft.EntityFrameworkCore.Migrations;

namespace Ensek_Api_Test.Migrations
{
    public partial class AddCustomerIdToMeterReading : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeterReadings_Customers_CustomerId",
                table: "MeterReadings");

            migrationBuilder.AlterColumn<string>(
                name: "MeterReadingValue",
                table: "MeterReadings",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "MeterReadings",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MeterReadings_Customers_CustomerId",
                table: "MeterReadings",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeterReadings_Customers_CustomerId",
                table: "MeterReadings");

            migrationBuilder.AlterColumn<int>(
                name: "MeterReadingValue",
                table: "MeterReadings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 5);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "MeterReadings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_MeterReadings_Customers_CustomerId",
                table: "MeterReadings",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
