using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class orders_clients_navigation_prop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                schema: "ordering",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_orders_ClientId",
                schema: "ordering",
                table: "orders",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_clients_ClientId",
                schema: "ordering",
                table: "orders",
                column: "ClientId",
                principalSchema: "ordering",
                principalTable: "clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_clients_ClientId",
                schema: "ordering",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_ClientId",
                schema: "ordering",
                table: "orders");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                schema: "ordering",
                table: "orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
