﻿// <auto-generated />
using System;
using Demo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Demo.Migrations
{
    [DbContext(typeof(DB))]
    [Migration("20240512090521_CreateDB")]
    partial class CreateDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Demo.Models.Attendance", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAttend")
                        .HasMaxLength(1)
                        .HasColumnType("bit");

                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("Demo.Models.Class", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("ClassType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("Demo.Models.ClassSubject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClassId")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ClassestId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DayOfWeek")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("Time");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("Time");

                    b.Property<string>("StudentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubjectId")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("ClassId");

                    b.HasIndex("SubjectId");

                    b.ToTable("ClassesSubjects");
                });

            modelBuilder.Entity("Demo.Models.Subject", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Fees")
                        .HasPrecision(6, 2)
                        .HasColumnType("decimal(6,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TutorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("TutorId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("Demo.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Age")
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PhotoURL")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Demo.Models.Admin", b =>
                {
                    b.HasBaseType("Demo.Models.User");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("Demo.Models.Parent", b =>
                {
                    b.HasBaseType("Demo.Models.User");

                    b.HasDiscriminator().HasValue("Parent");
                });

            modelBuilder.Entity("Demo.Models.Student", b =>
                {
                    b.HasBaseType("Demo.Models.User");

                    b.Property<string>("ClassId")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ClassesId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("ClassId");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("Demo.Models.Tutor", b =>
                {
                    b.HasBaseType("Demo.Models.User");

                    b.HasDiscriminator().HasValue("Tutor");
                });

            modelBuilder.Entity("Demo.Models.Attendance", b =>
                {
                    b.HasOne("Demo.Models.Student", "Student")
                        .WithMany("Attendances")
                        .HasForeignKey("StudentId");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Demo.Models.ClassSubject", b =>
                {
                    b.HasOne("Demo.Models.Class", "Class")
                        .WithMany("ClassSubjects")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Demo.Models.Subject", "Subject")
                        .WithMany("ClassesSubjects")
                        .HasForeignKey("SubjectId");

                    b.Navigation("Class");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Demo.Models.Subject", b =>
                {
                    b.HasOne("Demo.Models.Tutor", "Tutor")
                        .WithMany("Subjects")
                        .HasForeignKey("TutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tutor");
                });

            modelBuilder.Entity("Demo.Models.Student", b =>
                {
                    b.HasOne("Demo.Models.Class", "Class")
                        .WithMany("Students")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");
                });

            modelBuilder.Entity("Demo.Models.Class", b =>
                {
                    b.Navigation("ClassSubjects");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("Demo.Models.Subject", b =>
                {
                    b.Navigation("ClassesSubjects");
                });

            modelBuilder.Entity("Demo.Models.Student", b =>
                {
                    b.Navigation("Attendances");
                });

            modelBuilder.Entity("Demo.Models.Tutor", b =>
                {
                    b.Navigation("Subjects");
                });
#pragma warning restore 612, 618
        }
    }
}