﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project.BookingService.Data.Configurations;

#nullable disable

namespace Project.BookingService.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230724063347_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Project.BookingService.Data.BookingDoctor", b =>
                {
                    b.Property<Guid>("BookingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<int>("BookingStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("BookingTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("BookingType")
                        .HasColumnType("int");

                    b.Property<Guid>("DoctorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<Guid>("ProfileID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoomID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ScheduleID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BookingID");

                    b.HasIndex("ScheduleID")
                        .IsUnique();

                    b.ToTable("BookingDoctor", (string)null);
                });

            modelBuilder.Entity("Project.BookingService.Data.BookingPackage", b =>
                {
                    b.Property<Guid>("BookingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("AppoinmentTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("BookingStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("BookingTime")
                        .HasColumnType("datetime2");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<Guid>("ProfileID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ServicePackageID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BookingID");

                    b.ToTable("BookingPackage", (string)null);
                });

            modelBuilder.Entity("Project.BookingService.Data.DoctorCalendar", b =>
                {
                    b.Property<Guid>("CalenderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<Guid>("DoctorID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("CalenderID");

                    b.ToTable("DoctorCalendar", (string)null);
                });

            modelBuilder.Entity("Project.BookingService.Data.DoctorSchedule", b =>
                {
                    b.Property<Guid>("ScheduleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<Guid>("CalendarID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ScheduleID");

                    b.HasIndex("CalendarID");

                    b.ToTable("DoctorSchedule", (string)null);
                });

            modelBuilder.Entity("Project.BookingService.Data.BookingDoctor", b =>
                {
                    b.HasOne("Project.BookingService.Data.DoctorSchedule", "DoctorSchedule")
                        .WithOne("BookingDoctor")
                        .HasForeignKey("Project.BookingService.Data.BookingDoctor", "ScheduleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("PK_DoctorSchedule_One_To_One_BookingDoctor");

                    b.Navigation("DoctorSchedule");
                });

            modelBuilder.Entity("Project.BookingService.Data.DoctorSchedule", b =>
                {
                    b.HasOne("Project.BookingService.Data.DoctorCalendar", "DoctorCalendar")
                        .WithMany("DoctorSchedules")
                        .HasForeignKey("CalendarID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("PK_DoctorCalendar_One_To_Many_DoctorSchedule");

                    b.Navigation("DoctorCalendar");
                });

            modelBuilder.Entity("Project.BookingService.Data.DoctorCalendar", b =>
                {
                    b.Navigation("DoctorSchedules");
                });

            modelBuilder.Entity("Project.BookingService.Data.DoctorSchedule", b =>
                {
                    b.Navigation("BookingDoctor");
                });
#pragma warning restore 612, 618
        }
    }
}
