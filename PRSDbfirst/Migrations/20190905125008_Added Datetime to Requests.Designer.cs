﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PRSDbfirst.Models;

namespace PRSDbfirst.Migrations
{
    [DbContext(typeof(PRSDbContext))]
    [Migration("20190905125008_Added Datetime to Requests")]
    partial class AddedDatetimetoRequests
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PRSDbfirst.Models.Products", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("PartNbr")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("PhotoPath")
                        .HasMaxLength(255);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 0)");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<int>("VendorId");

                    b.HasKey("Id");

                    b.HasIndex("PartNbr")
                        .IsUnique()
                        .HasName("IX_Products_PartNbr_Unique");

                    b.HasIndex("VendorId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("PRSDbfirst.Models.RequestLines", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProductId");

                    b.Property<int>("Quantity")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("((1))");

                    b.Property<int>("RequestId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("RequestId");

                    b.ToTable("RequestLines");
                });

            modelBuilder.Entity("PRSDbfirst.Models.Requests", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateRequested");

                    b.Property<string>("DeliveryMode")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('pickup')")
                        .HasMaxLength(20);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<string>("Justification")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("('new')")
                        .HasMaxLength(10);

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18, 0)");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("PRSDbfirst.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasMaxLength(255);

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<bool>("IsAdmin");

                    b.Property<bool>("IsReviewer");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Phone")
                        .HasMaxLength(15);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasName("IX_Users_Username_Unique");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PRSDbfirst.Models.Vendors", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Email")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.Property<string>("Phone")
                        .HasMaxLength(15);

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(3);

                    b.Property<string>("Zip")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique()
                        .HasName("IX_Vendors_Code_Unique");

                    b.ToTable("Vendors");
                });

            modelBuilder.Entity("PRSDbfirst.Models.Products", b =>
                {
                    b.HasOne("PRSDbfirst.Models.Vendors", "Vendor")
                        .WithMany("Products")
                        .HasForeignKey("VendorId")
                        .HasConstraintName("FK__Products__Vendor__3D5E1FD2");
                });

            modelBuilder.Entity("PRSDbfirst.Models.RequestLines", b =>
                {
                    b.HasOne("PRSDbfirst.Models.Products", "Product")
                        .WithMany("RequestLines")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK__RequestLi__Produ__46E78A0C");

                    b.HasOne("PRSDbfirst.Models.Requests", "Request")
                        .WithMany("RequestLines")
                        .HasForeignKey("RequestId")
                        .HasConstraintName("FK__RequestLi__Reque__45F365D3");
                });

            modelBuilder.Entity("PRSDbfirst.Models.Requests", b =>
                {
                    b.HasOne("PRSDbfirst.Models.Users", "User")
                        .WithMany("Requests")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Requests__UserId__4316F928");
                });
#pragma warning restore 612, 618
        }
    }
}
