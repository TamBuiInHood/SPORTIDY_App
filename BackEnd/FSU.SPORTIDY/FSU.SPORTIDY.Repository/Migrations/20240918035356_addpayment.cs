using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSU.SPORTIDY.Repository.Migrations
{
    /// <inheritdoc />
    public partial class addpayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankCode",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequestBy",
                table: "Friendship",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: true),
                    DateOfTransaction = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    BookingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Payment__7A41AF1C1AA63361", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK__Payment__Booking__59063A47",
                        column: x => x.BookingId,
                        principalTable: "Booking",
                        principalColumn: "BookingID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_BookingId",
                table: "Payment",
                column: "BookingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropColumn(
                name: "BankCode",
                table: "User");

            migrationBuilder.DropColumn(
                name: "BankName",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RequestBy",
                table: "Friendship");
        }
    }
}
