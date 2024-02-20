﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ordering.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ordering");

            migrationBuilder.CreateSequence(
                name: "clientseq",
                schema: "ordering",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "deliveryseq",
                schema: "ordering",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "orderitemseq",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "orderseq",
                schema: "ordering",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "clients",
                schema: "ordering",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    IdentityGuid = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "orderstatus",
                schema: "ordering",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderstatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                schema: "ordering",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    PickupAddress_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PickupAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PickupAddress_State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PickupAddress_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PickupAddress_ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DropoffAddress_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DropoffAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DropoffAddress_State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DropoffAddress_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DropoffAddress_ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderStatusId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orders_orderstatus_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalSchema: "ordering",
                        principalTable: "orderstatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "deliveries",
                schema: "ordering",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false, computedColumnSql: "_gasCost + _tollsCost + _additionalCosts"),
                    AdditionalCosts = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DeliveryDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Distance = table.Column<double>(type: "float", nullable: false),
                    ElapsedTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndOdometer = table.Column<double>(type: "float", nullable: false),
                    GasCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Route = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    StartOdometer = table.Column<double>(type: "float", nullable: false),
                    TollsCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_deliveries_orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "ordering",
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "loads",
                schema: "ordering",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Units = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_loads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_loads_orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "ordering",
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_clients_IdentityGuid",
                schema: "ordering",
                table: "clients",
                column: "IdentityGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_deliveries_OrderId",
                schema: "ordering",
                table: "deliveries",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_loads_OrderId",
                schema: "ordering",
                table: "loads",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_OrderStatusId",
                schema: "ordering",
                table: "orders",
                column: "OrderStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "clients",
                schema: "ordering");

            migrationBuilder.DropTable(
                name: "deliveries",
                schema: "ordering");

            migrationBuilder.DropTable(
                name: "loads",
                schema: "ordering");

            migrationBuilder.DropTable(
                name: "orders",
                schema: "ordering");

            migrationBuilder.DropTable(
                name: "orderstatus",
                schema: "ordering");

            migrationBuilder.DropSequence(
                name: "clientseq",
                schema: "ordering");

            migrationBuilder.DropSequence(
                name: "deliveryseq",
                schema: "ordering");

            migrationBuilder.DropSequence(
                name: "orderitemseq");

            migrationBuilder.DropSequence(
                name: "orderseq",
                schema: "ordering");
        }
    }
}
