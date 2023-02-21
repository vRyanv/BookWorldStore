﻿// <auto-generated />
using System;
using BookWorldStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookWorldStore.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BookWorldStore.Models.Book", b =>
                {
                    b.Property<int>("book_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("book_id"), 1L, 1);

                    b.Property<string>("author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("cate_id")
                        .HasColumnType("int");

                    b.Property<string>("des")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("inventory_num")
                        .HasColumnType("int");

                    b.Property<float>("price")
                        .HasColumnType("real");

                    b.Property<string>("publishing_year")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("status")
                        .HasColumnType("int");

                    b.Property<int>("sup_id")
                        .HasColumnType("int");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("book_id");

                    b.HasIndex("cate_id");

                    b.HasIndex("sup_id");

                    b.ToTable("books");
                });

            modelBuilder.Entity("BookWorldStore.Models.Category", b =>
                {
                    b.Property<int>("cate_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("cate_id"), 1L, 1);

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("cate_id");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("BookWorldStore.Models.Order", b =>
                {
                    b.Property<int>("order_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("order_id"), 1L, 1);

                    b.Property<DateTime>("delivery_date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("order_date")
                        .HasColumnType("datetime2");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.Property<float>("total")
                        .HasColumnType("real");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("order_id");

                    b.HasIndex("user_id");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("BookWorldStore.Models.OrderDetail", b =>
                {
                    b.Property<int>("order_detail_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("order_detail_id"), 1L, 1);

                    b.Property<int>("book_id")
                        .HasColumnType("int");

                    b.Property<int>("order_id")
                        .HasColumnType("int");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.HasKey("order_detail_id");

                    b.HasIndex("book_id");

                    b.HasIndex("order_id");

                    b.ToTable("orderDetails");
                });

            modelBuilder.Entity("BookWorldStore.Models.Requirement", b =>
                {
                    b.Property<int>("req_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("req_id"), 1L, 1);

                    b.Property<int>("cate_id")
                        .HasColumnType("int");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("req_id");

                    b.HasIndex("cate_id");

                    b.ToTable("requirements");
                });

            modelBuilder.Entity("BookWorldStore.Models.Supplier", b =>
                {
                    b.Property<int>("sup_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("sup_id"), 1L, 1);

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("sup_id");

                    b.ToTable("suppliers");
                });

            modelBuilder.Entity("BookWorldStore.Models.User", b =>
                {
                    b.Property<int>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("user_id"), 1L, 1);

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("status")
                        .HasColumnType("int");

                    b.Property<string>("token_reset_pass")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("user_id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("BookWorldStore.Models.Book", b =>
                {
                    b.HasOne("BookWorldStore.Models.Category", "category")
                        .WithMany("books")
                        .HasForeignKey("cate_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookWorldStore.Models.Supplier", "supplier")
                        .WithMany("books")
                        .HasForeignKey("sup_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("category");

                    b.Navigation("supplier");
                });

            modelBuilder.Entity("BookWorldStore.Models.Order", b =>
                {
                    b.HasOne("BookWorldStore.Models.User", "user")
                        .WithMany("orders")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("user");
                });

            modelBuilder.Entity("BookWorldStore.Models.OrderDetail", b =>
                {
                    b.HasOne("BookWorldStore.Models.Book", "book")
                        .WithMany()
                        .HasForeignKey("book_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookWorldStore.Models.Order", "order")
                        .WithMany()
                        .HasForeignKey("order_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("book");

                    b.Navigation("order");
                });

            modelBuilder.Entity("BookWorldStore.Models.Requirement", b =>
                {
                    b.HasOne("BookWorldStore.Models.Category", "category")
                        .WithMany()
                        .HasForeignKey("cate_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("category");
                });

            modelBuilder.Entity("BookWorldStore.Models.Category", b =>
                {
                    b.Navigation("books");
                });

            modelBuilder.Entity("BookWorldStore.Models.Supplier", b =>
                {
                    b.Navigation("books");
                });

            modelBuilder.Entity("BookWorldStore.Models.User", b =>
                {
                    b.Navigation("orders");
                });
#pragma warning restore 612, 618
        }
    }
}
