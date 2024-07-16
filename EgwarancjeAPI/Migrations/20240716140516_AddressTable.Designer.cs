﻿// <auto-generated />
using System;
using EgwarancjeAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EgwarancjeAPI.Migrations
{
    [DbContext(typeof(LocalDatabaseContext))]
    [Migration("20240716140516_AddressTable")]
    partial class AddressTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.2.24128.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EgwarancjeDbLibrary.Models.Address", b =>
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

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("EgwarancjeDbLibrary.Models.Attachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WarrantySpecId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WarrantySpecId");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("EgwarancjeDbLibrary.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OrderDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("EgwarancjeDbLibrary.Models.OrderSpec", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("Realization")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("ValueGross")
                        .HasColumnType("real");

                    b.Property<float>("ValueNet")
                        .HasColumnType("real");

                    b.Property<int>("WarrantyLength")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrdersSpec");
                });

            modelBuilder.Entity("EgwarancjeDbLibrary.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConfirmationToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EgwarancjeDbLibrary.Models.Warranty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfWarranty")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("Warranties");
                });

            modelBuilder.Entity("EgwarancjeDbLibrary.Models.WarrantySpec", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderSpecId")
                        .HasColumnType("int");

                    b.Property<int>("WarrantyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderSpecId");

                    b.HasIndex("WarrantyId");

                    b.ToTable("WarrantiesSpecs");
                });

            modelBuilder.Entity("EgwarancjeDbLibrary.Models.Address", b =>
                {
                    b.HasOne("EgwarancjeDbLibrary.Models.User", "User")
                        .WithMany("Addresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EgwarancjeDbLibrary.Models.Attachment", b =>
                {
                    b.HasOne("EgwarancjeDbLibrary.Models.WarrantySpec", "WarrantySpec")
                        .WithMany("Attachments")
                        .HasForeignKey("WarrantySpecId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("WarrantySpec");
                });

            modelBuilder.Entity("EgwarancjeDbLibrary.Models.Order", b =>
                {
                    b.HasOne("EgwarancjeDbLibrary.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("EgwarancjeDbLibrary.Models.OrderSpec", b =>
                {
                    b.HasOne("EgwarancjeDbLibrary.Models.Order", "Order")
                        .WithMany("OrderSpecs")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("EgwarancjeDbLibrary.Models.Warranty", b =>
                {
                    b.HasOne("EgwarancjeDbLibrary.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EgwarancjeDbLibrary.Models.User", null)
                        .WithMany("Warranties")
                        .HasForeignKey("UserId");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("EgwarancjeDbLibrary.Models.WarrantySpec", b =>
                {
                    b.HasOne("EgwarancjeDbLibrary.Models.OrderSpec", "OrderSpec")
                        .WithMany()
                        .HasForeignKey("OrderSpecId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("EgwarancjeDbLibrary.Models.Warranty", "Warranty")
                        .WithMany("WarrantySpecs")
                        .HasForeignKey("WarrantyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderSpec");

                    b.Navigation("Warranty");
                });

            modelBuilder.Entity("EgwarancjeDbLibrary.Models.Order", b =>
                {
                    b.Navigation("OrderSpecs");
                });

            modelBuilder.Entity("EgwarancjeDbLibrary.Models.User", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Orders");

                    b.Navigation("Warranties");
                });

            modelBuilder.Entity("EgwarancjeDbLibrary.Models.Warranty", b =>
                {
                    b.Navigation("WarrantySpecs");
                });

            modelBuilder.Entity("EgwarancjeDbLibrary.Models.WarrantySpec", b =>
                {
                    b.Navigation("Attachments");
                });
#pragma warning restore 612, 618
        }
    }
}
