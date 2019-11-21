﻿// <auto-generated />
using System;
using JMS.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JMS.API.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20191117205849_enhancelatlng")]
    partial class enhancelatlng
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("JMS.DAL.Models.AssessmentQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<int?>("CheckpointId")
                        .HasColumnType("int");

                    b.Property<bool>("IsThirdParty")
                        .HasColumnType("bit");

                    b.Property<string>("Question")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CheckpointId");

                    b.ToTable("AssessmentQuestion");
                });

            modelBuilder.Entity("JMS.DAL.Models.AssessmentResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsYes")
                        .HasColumnType("bit");

                    b.Property<int?>("JourneyUpdateId")
                        .HasColumnType("int");

                    b.Property<int?>("QuestionId")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("VehicleNo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("JourneyUpdateId");

                    b.HasIndex("QuestionId");

                    b.HasIndex("UserId");

                    b.ToTable("AssessmentResult");
                });

            modelBuilder.Entity("JMS.DAL.Models.Checkpoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsThirdParty")
                        .HasColumnType("bit");

                    b.Property<double?>("Latitude")
                        .HasColumnType("float");

                    b.Property<double?>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Checkpoint");
                });

            modelBuilder.Entity("JMS.DAL.Models.CodeException", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AssemblyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssemblyVersion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClassName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ExceptionTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("MachineName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MethodName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StackDump")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CodeException");
                });

            modelBuilder.Entity("JMS.DAL.Models.Journey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CargoPriority")
                        .HasColumnType("int");

                    b.Property<int>("CargoSeverity")
                        .HasColumnType("int");

                    b.Property<string>("CargoType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("CargoWeight")
                        .HasColumnType("float");

                    b.Property<DateTime?>("DeliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FromDestination")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("FromLat")
                        .HasColumnType("float");

                    b.Property<double?>("FromLng")
                        .HasColumnType("float");

                    b.Property<bool>("IsThirdParty")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTruckTransport")
                        .HasColumnType("bit");

                    b.Property<int>("JourneyStatus")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ToDistination")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("ToLat")
                        .HasColumnType("float");

                    b.Property<double?>("ToLng")
                        .HasColumnType("float");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Journey");
                });

            modelBuilder.Entity("JMS.DAL.Models.JourneyUpdate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CheckpointId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("DriverId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAlert")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDriverStatus")
                        .HasColumnType("bit");

                    b.Property<bool>("IsJourneyCheckpoint")
                        .HasColumnType("bit");

                    b.Property<int?>("JourneyId")
                        .HasColumnType("int");

                    b.Property<int>("JourneyStatus")
                        .HasColumnType("int");

                    b.Property<double?>("Latitude")
                        .HasColumnType("float");

                    b.Property<double?>("Longitude")
                        .HasColumnType("float");

                    b.Property<int>("RiskLevel")
                        .HasColumnType("int");

                    b.Property<string>("StatusMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("VehicleNo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CheckpointId");

                    b.HasIndex("JourneyId");

                    b.HasIndex("UserId");

                    b.ToTable("JourneyUpdate");
                });

            modelBuilder.Entity("JMS.DAL.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRead")
                        .HasColumnType("bit");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("JMS.DAL.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("JMS.DAL.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("GatePassStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LicenseExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LicenseNo")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("TrainingDetails")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserWorkForceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UserGroupId");

                    b.HasIndex("UserWorkForceId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("JMS.DAL.Models.UserGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("JMS.DAL.Models.UserRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("JMS.DAL.Models.UserWorkForce", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.ToTable("UserWorkForces");
                });

            modelBuilder.Entity("JMS.DAL.Models.AssessmentQuestion", b =>
                {
                    b.HasOne("JMS.DAL.Models.Checkpoint", "Checkpoint")
                        .WithMany()
                        .HasForeignKey("CheckpointId");
                });

            modelBuilder.Entity("JMS.DAL.Models.AssessmentResult", b =>
                {
                    b.HasOne("JMS.DAL.Models.JourneyUpdate", "JourneyUpdate")
                        .WithMany("AssessmentResult")
                        .HasForeignKey("JourneyUpdateId");

                    b.HasOne("JMS.DAL.Models.AssessmentQuestion", "Question")
                        .WithMany("AssessmentResult")
                        .HasForeignKey("QuestionId");

                    b.HasOne("JMS.DAL.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JMS.DAL.Models.Journey", b =>
                {
                    b.HasOne("JMS.DAL.Models.User", "User")
                        .WithMany("Journeys")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JMS.DAL.Models.JourneyUpdate", b =>
                {
                    b.HasOne("JMS.DAL.Models.Checkpoint", "Checkpoint")
                        .WithMany("JourneyUpdate")
                        .HasForeignKey("CheckpointId");

                    b.HasOne("JMS.DAL.Models.Journey", "Journey")
                        .WithMany("JourneyUpdate")
                        .HasForeignKey("JourneyId");

                    b.HasOne("JMS.DAL.Models.User", "User")
                        .WithMany("JourneyUpdates")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("JMS.DAL.Models.Notification", b =>
                {
                    b.HasOne("JMS.DAL.Models.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JMS.DAL.Models.User", b =>
                {
                    b.HasOne("JMS.DAL.Models.UserGroup", "UserGroup")
                        .WithMany("Users")
                        .HasForeignKey("UserGroupId");

                    b.HasOne("JMS.DAL.Models.UserWorkForce", "UserWorkForce")
                        .WithMany("Users")
                        .HasForeignKey("UserWorkForceId");
                });

            modelBuilder.Entity("JMS.DAL.Models.UserRole", b =>
                {
                    b.HasOne("JMS.DAL.Models.Role", "Role")
                        .WithMany("UsersRole")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JMS.DAL.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
