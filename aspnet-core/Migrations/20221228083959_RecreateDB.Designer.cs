﻿// <auto-generated />
using System;
using BookProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookProject.Migrations
{
    [DbContext(typeof(BooksContext))]
    [Migration("20221228083959_RecreateDB")]
    partial class RecreateDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookProject.Models.BookInfo", b =>
                {
                    b.Property<Guid>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("ID sách");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("Tác giả");

                    b.Property<Guid>("CategoryId")
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("smalldatetime")
                        .HasComment("Thời gian tạo");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("smalldatetime")
                        .HasComment("Thời gian chỉnh sửa");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComment("Tên/ Tựa đề sách");

                    b.HasKey("BookId")
                        .HasName("PK_BookInfo_BookId");

                    b.HasIndex("CategoryId");

                    b.ToTable("BookInfo", (string)null);
                });

            modelBuilder.Entity("BookProject.Models.BookPage", b =>
                {
                    b.Property<Guid>("PageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Content")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<short>("PageNumber")
                        .HasColumnType("smallint");

                    b.HasKey("PageId")
                        .HasName("PK_BookPages_PageId");

                    b.HasIndex("BookId");

                    b.ToTable("BookPages", t =>
                        {
                            t.HasComment("Các trang sách");
                        });
                });

            modelBuilder.Entity("BookProject.Models.Category", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(36)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories", t =>
                        {
                            t.HasComment("Các loại sách");
                        });
                });

            modelBuilder.Entity("BookProject.Models.BookInfo", b =>
                {
                    b.HasOne("BookProject.Models.Category", "Category")
                        .WithMany("BookInfos")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("BookProject.Models.BookPage", b =>
                {
                    b.HasOne("BookProject.Models.BookInfo", "Book")
                        .WithMany("BookPages")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_BookPages_BookId");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("BookProject.Models.BookInfo", b =>
                {
                    b.Navigation("BookPages");
                });

            modelBuilder.Entity("BookProject.Models.Category", b =>
                {
                    b.Navigation("BookInfos");
                });
#pragma warning restore 612, 618
        }
    }
}
