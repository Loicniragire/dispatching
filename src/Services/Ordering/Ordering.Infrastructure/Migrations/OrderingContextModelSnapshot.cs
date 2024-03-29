﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ordering.Infrastructure;

#nullable disable

namespace Ordering.Infrastructure.Migrations
{
    [DbContext(typeof(OrderingContext))]
    partial class OrderingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.HasSequence("clientseq", "ordering")
                .IncrementsBy(10);

            modelBuilder.HasSequence("deliveryseq", "ordering")
                .IncrementsBy(10);

            modelBuilder.HasSequence("orderitemseq")
                .IncrementsBy(10);

            modelBuilder.HasSequence("orderseq", "ordering")
                .IncrementsBy(10);

            modelBuilder.Entity("Load", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "orderitemseq");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<decimal>("_discount")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Discount");

                    b.Property<string>("_productName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ProductName");

                    b.Property<decimal>("_unitPrice")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("UnitPrice");

                    b.Property<int>("_units")
                        .HasColumnType("int")
                        .HasColumnName("Units");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("loads", "ordering");
                });

            modelBuilder.Entity("Ordering.Domain.AggregatesModel.ClientAggregate.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "clientseq", "ordering");

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentityGuid")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RemovedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("IdentityGuid")
                        .IsUnique();

                    b.ToTable("clients", "ordering");
                });

            modelBuilder.Entity("Ordering.Domain.AggregatesModel.OrderAggregate.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Ordering.Domain.AggregatesModel.OrderAggregate.Delivery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "deliveryseq", "ordering");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("_additionalCosts")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("AdditionalCosts");

                    b.Property<DateTimeOffset>("_deliveryDate")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("DeliveryDate");

                    b.Property<double>("_distance")
                        .HasColumnType("float")
                        .HasColumnName("Distance");

                    b.Property<TimeSpan>("_elapsedTime")
                        .HasColumnType("time")
                        .HasColumnName("ElapsedTime");

                    b.Property<double>("_endOdometer")
                        .HasColumnType("float")
                        .HasColumnName("EndOdometer");

                    b.Property<decimal>("_gasCost")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("GasCost");

                    b.Property<string>("_route")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Route");

                    b.Property<DateTimeOffset>("_startDate")
                        .HasColumnType("datetimeoffset")
                        .HasColumnName("StartDate");

                    b.Property<double>("_startOdometer")
                        .HasColumnType("float")
                        .HasColumnName("StartOdometer");

                    b.Property<decimal>("_tollsCost")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("TollsCost");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("deliveries", "ordering");
                });

            modelBuilder.Entity("Ordering.Domain.AggregatesModel.OrderAggregate.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseHiLo(b.Property<int>("Id"), "orderseq", "ordering");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int?>("DropoffAddressId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("PickupAddressId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("_description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Description");

                    b.Property<DateTime>("_orderDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("OrderDate");

                    b.Property<int>("_orderStatusId")
                        .HasColumnType("int")
                        .HasColumnName("OrderStatusId");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("DropoffAddressId");

                    b.HasIndex("PickupAddressId");

                    b.HasIndex("_orderStatusId");

                    b.ToTable("orders", "ordering");
                });

            modelBuilder.Entity("Ordering.Domain.AggregatesModel.OrderAggregate.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("orderstatus", "ordering");
                });

            modelBuilder.Entity("Load", b =>
                {
                    b.HasOne("Ordering.Domain.AggregatesModel.OrderAggregate.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Ordering.Domain.AggregatesModel.ClientAggregate.Client", b =>
                {
                    b.HasOne("Ordering.Domain.AggregatesModel.OrderAggregate.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Ordering.Domain.AggregatesModel.OrderAggregate.Delivery", b =>
                {
                    b.HasOne("Ordering.Domain.AggregatesModel.OrderAggregate.Order", null)
                        .WithOne()
                        .HasForeignKey("Ordering.Domain.AggregatesModel.OrderAggregate.Delivery", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Ordering.Domain.AggregatesModel.OrderAggregate.Order", b =>
                {
                    b.HasOne("Ordering.Domain.AggregatesModel.ClientAggregate.Client", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ordering.Domain.AggregatesModel.OrderAggregate.Address", "DropoffAddress")
                        .WithMany()
                        .HasForeignKey("DropoffAddressId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ordering.Domain.AggregatesModel.OrderAggregate.Address", "PickupAddress")
                        .WithMany()
                        .HasForeignKey("PickupAddressId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Ordering.Domain.AggregatesModel.OrderAggregate.OrderStatus", "OrderStatus")
                        .WithMany()
                        .HasForeignKey("_orderStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("DropoffAddress");

                    b.Navigation("OrderStatus");

                    b.Navigation("PickupAddress");
                });

            modelBuilder.Entity("Ordering.Domain.AggregatesModel.ClientAggregate.Client", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Ordering.Domain.AggregatesModel.OrderAggregate.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
