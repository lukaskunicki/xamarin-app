﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApi.DAL;

#nullable disable

namespace WebApi.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20220607194002_ticket-update")]
    partial class ticketupdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebApi.Model.Client", b =>
                {
                    b.Property<int>("clientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("clientId"), 1L, 1);

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("responsibleEmployeeemployeeId")
                        .HasColumnType("int");

                    b.HasKey("clientId");

                    b.HasIndex("responsibleEmployeeemployeeId");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("WebApi.Model.Comment", b =>
                {
                    b.Property<int>("commentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("commentId"), 1L, 1);

                    b.Property<int?>("assignedEmployeeemployeeId")
                        .HasColumnType("int");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("created")
                        .HasColumnType("datetime2");

                    b.HasKey("commentId");

                    b.HasIndex("assignedEmployeeemployeeId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("WebApi.Model.Employee", b =>
                {
                    b.Property<int>("employeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("employeeId"), 1L, 1);

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("teamId")
                        .HasColumnType("int");

                    b.HasKey("employeeId");

                    b.HasIndex("teamId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("WebApi.Model.Priority", b =>
                {
                    b.Property<int>("priorityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("priorityId"), 1L, 1);

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.HasKey("priorityId");

                    b.ToTable("Priority");
                });

            modelBuilder.Entity("WebApi.Model.Project", b =>
                {
                    b.Property<int>("projectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("projectId"), 1L, 1);

                    b.Property<int?>("clientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("endTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("projectManageremployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("startTime")
                        .HasColumnType("datetime2");

                    b.HasKey("projectId");

                    b.HasIndex("clientId");

                    b.HasIndex("projectManageremployeeId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("WebApi.Model.Sprint", b =>
                {
                    b.Property<int>("sprintId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("sprintId"), 1L, 1);

                    b.Property<DateTime>("endTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("startTime")
                        .HasColumnType("datetime2");

                    b.HasKey("sprintId");

                    b.ToTable("Sprint");
                });

            modelBuilder.Entity("WebApi.Model.Tag", b =>
                {
                    b.Property<int>("tagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("tagId"), 1L, 1);

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("tagId");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("WebApi.Model.Team", b =>
                {
                    b.Property<int>("teamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("teamId"), 1L, 1);

                    b.Property<string>("teamDescription")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("teamName")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.HasKey("teamId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("WebApi.Model.Ticket", b =>
                {
                    b.Property<int>("ticketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ticketId"), 1L, 1);

                    b.Property<int?>("assignedEmployeeemployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("created")
                        .HasColumnType("datetime2");

                    b.Property<int>("priorityId")
                        .HasColumnType("int");

                    b.Property<int?>("reporteremployeeId")
                        .HasColumnType("int");

                    b.Property<int?>("sprintId")
                        .HasColumnType("int");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("ticketId");

                    b.HasIndex("assignedEmployeeemployeeId");

                    b.HasIndex("priorityId");

                    b.HasIndex("reporteremployeeId");

                    b.HasIndex("sprintId");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("WebApi.Model.Client", b =>
                {
                    b.HasOne("WebApi.Model.Employee", "responsibleEmployee")
                        .WithMany()
                        .HasForeignKey("responsibleEmployeeemployeeId");

                    b.Navigation("responsibleEmployee");
                });

            modelBuilder.Entity("WebApi.Model.Comment", b =>
                {
                    b.HasOne("WebApi.Model.Employee", "assignedEmployee")
                        .WithMany()
                        .HasForeignKey("assignedEmployeeemployeeId");

                    b.Navigation("assignedEmployee");
                });

            modelBuilder.Entity("WebApi.Model.Employee", b =>
                {
                    b.HasOne("WebApi.Model.Team", "team")
                        .WithMany()
                        .HasForeignKey("teamId");

                    b.Navigation("team");
                });

            modelBuilder.Entity("WebApi.Model.Project", b =>
                {
                    b.HasOne("WebApi.Model.Client", "client")
                        .WithMany()
                        .HasForeignKey("clientId");

                    b.HasOne("WebApi.Model.Employee", "projectManager")
                        .WithMany()
                        .HasForeignKey("projectManageremployeeId");

                    b.Navigation("client");

                    b.Navigation("projectManager");
                });

            modelBuilder.Entity("WebApi.Model.Ticket", b =>
                {
                    b.HasOne("WebApi.Model.Employee", "assignedEmployee")
                        .WithMany()
                        .HasForeignKey("assignedEmployeeemployeeId");

                    b.HasOne("WebApi.Model.Priority", "priority")
                        .WithMany()
                        .HasForeignKey("priorityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApi.Model.Employee", "reporter")
                        .WithMany()
                        .HasForeignKey("reporteremployeeId");

                    b.HasOne("WebApi.Model.Sprint", "sprint")
                        .WithMany()
                        .HasForeignKey("sprintId");

                    b.Navigation("assignedEmployee");

                    b.Navigation("priority");

                    b.Navigation("reporter");

                    b.Navigation("sprint");
                });
#pragma warning restore 612, 618
        }
    }
}