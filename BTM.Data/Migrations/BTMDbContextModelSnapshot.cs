﻿// <auto-generated />
using System;
using BTM.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BTM.Data.Migrations
{
    [DbContext(typeof(BTMDbContext))]
    partial class BTMDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BTM.Data.Counters", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("BlackWhiteCounter")
                        .HasColumnType("int");

                    b.Property<int>("ColorCounter")
                        .HasColumnType("int");

                    b.Property<int>("CounterSum")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("DeviceID")
                        .HasColumnType("int");

                    b.Property<int>("Quartal")
                        .HasColumnType("int");

                    b.Property<int>("QuartalYear")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Counters");
                });

            modelBuilder.Entity("BTM.Data.Devices", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeviceNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FreePrintsBlackWhite")
                        .HasColumnType("int");

                    b.Property<int>("FreePrintsColor")
                        .HasColumnType("int");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("bit");

                    b.Property<int>("KundenID")
                        .HasColumnType("int");

                    b.Property<string>("Standort")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VertragsID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("BTM.Data.Kunde", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("InvoiceIsCreated")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Standort")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("BTM.Data.Telefon", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("KundenID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelefonNummer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Telephone");
                });
#pragma warning restore 612, 618
        }
    }
}
