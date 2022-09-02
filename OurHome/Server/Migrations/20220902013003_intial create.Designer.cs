﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OurHome.Server;

#nullable disable

namespace OurHome.Server.Migrations
{
    [DbContext(typeof(OurHomeDbContext))]
    [Migration("20220902013003_intial create")]
    partial class intialcreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("OurHome.Server.Models.Bills", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Bill")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ID");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("OurHome.Server.Models.PersonsBills", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Bins")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Electricity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Internet")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Milk")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Oil")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Rent")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Name");

                    b.ToTable("PersonsBills");
                });
#pragma warning restore 612, 618
        }
    }
}
