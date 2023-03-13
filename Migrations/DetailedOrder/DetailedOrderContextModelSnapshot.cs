﻿// <auto-generated />
using DT191G_projekt.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DT191G_projekt.Migrations.DetailedOrder
{
    [DbContext(typeof(DetailedOrderContext))]
    partial class DetailedOrderContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("DT191G_projekt.Models.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ArticleNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DetailedOrderId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProductTitle")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("DetailedOrderId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("DT191G_projekt.Models.DetailedOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("DetailedOrder");
                });

            modelBuilder.Entity("DT191G_projekt.Models.Article", b =>
                {
                    b.HasOne("DT191G_projekt.Models.DetailedOrder", null)
                        .WithMany("Articles")
                        .HasForeignKey("DetailedOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DT191G_projekt.Models.DetailedOrder", b =>
                {
                    b.Navigation("Articles");
                });
#pragma warning restore 612, 618
        }
    }
}
