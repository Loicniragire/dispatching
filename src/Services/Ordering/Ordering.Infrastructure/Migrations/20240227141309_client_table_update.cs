using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class client_table_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                schema: "ordering",
                table: "clients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                schema: "ordering",
                table: "clients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "ordering",
                table: "clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                schema: "ordering",
                table: "clients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                schema: "ordering",
                table: "clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                schema: "ordering",
                table: "clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RemovedDate",
                schema: "ordering",
                table: "clients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                schema: "ordering",
                table: "clients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_clients_AddressId",
                schema: "ordering",
                table: "clients",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_clients_Addresses_AddressId",
                schema: "ordering",
                table: "clients",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_clients_Addresses_AddressId",
                schema: "ordering",
                table: "clients");

            migrationBuilder.DropIndex(
                name: "IX_clients_AddressId",
                schema: "ordering",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "AddressId",
                schema: "ordering",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                schema: "ordering",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "ordering",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                schema: "ordering",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "Notes",
                schema: "ordering",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "Phone",
                schema: "ordering",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "RemovedDate",
                schema: "ordering",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                schema: "ordering",
                table: "clients");
        }
    }
}
