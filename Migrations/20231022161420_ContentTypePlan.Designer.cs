﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SchoolHubApi.Data;

#nullable disable

namespace SchoolHubApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231022161420_ContentTypePlan")]
    partial class ContentTypePlan
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ParentPupil", b =>
                {
                    b.Property<int>("ChildrenId")
                        .HasColumnType("int");

                    b.Property<int>("ParentsId")
                        .HasColumnType("int");

                    b.HasKey("ChildrenId", "ParentsId");

                    b.HasIndex("ParentsId");

                    b.ToTable("ParentPupil");
                });

            modelBuilder.Entity("SchoolHubApi.Domain.Entities.Classroom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClassAccessCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Plan")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PlanContentType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClassAccessCode")
                        .IsUnique();

                    b.ToTable("Classrooms");
                });

            modelBuilder.Entity("SchoolHubApi.Domain.Entities.Parent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("UserDataEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserDataEmail");

                    b.ToTable("Parents");
                });

            modelBuilder.Entity("SchoolHubApi.Domain.Entities.Pupil", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccessCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ClassroomId")
                        .HasColumnType("int");

                    b.Property<string>("UserDataEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AccessCode")
                        .IsUnique();

                    b.HasIndex("ClassroomId");

                    b.HasIndex("UserDataEmail");

                    b.ToTable("Pupils");
                });

            modelBuilder.Entity("SchoolHubApi.Domain.Entities.ResetPasswordCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ResetCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ValidTo")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("ResetPasswordCodes");
                });

            modelBuilder.Entity("SchoolHubApi.Domain.Entities.Teacher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("UserDataEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserDataEmail");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("SchoolHubApi.Domain.Entities.UserData", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Pesel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Email");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Email = "schoolhubpl@gmail.com",
                            FirstName = "Admin",
                            LastName = "",
                            PasswordHash = new byte[] { 67, 58, 59, 74, 111, 168, 106, 72, 90, 93, 165, 77, 156, 118, 146, 133, 212, 110, 21, 201, 216, 155, 71, 218, 156, 24, 245, 211, 159, 105, 50, 126, 15, 250, 32, 229, 10, 166, 84, 94, 86, 20, 138, 95, 120, 62, 149, 79, 243, 191, 6, 6, 94, 95, 81, 145, 32, 184, 72, 69, 56, 41, 96, 212 },
                            PasswordSalt = new byte[] { 143, 231, 28, 232, 17, 148, 76, 250, 96, 174, 189, 1, 143, 221, 198, 223, 231, 199, 21, 116, 122, 50, 121, 104, 98, 67, 148, 114, 226, 134, 238, 96, 182, 125, 49, 31, 230, 198, 86, 233, 157, 55, 158, 105, 205, 104, 47, 157, 96, 175, 108, 149, 85, 208, 118, 129, 87, 176, 142, 38, 182, 237, 120, 5, 111, 120, 126, 89, 208, 225, 6, 233, 150, 245, 95, 72, 137, 36, 142, 60, 7, 65, 246, 248, 212, 143, 235, 120, 135, 117, 157, 70, 201, 28, 220, 89, 254, 80, 140, 101, 59, 178, 111, 128, 44, 83, 46, 59, 69, 182, 34, 184, 69, 156, 160, 32, 108, 186, 102, 78, 242, 143, 151, 239, 78, 149, 170, 184 },
                            Pesel = "",
                            PhoneNumber = "",
                            Role = "Admin"
                        });
                });

            modelBuilder.Entity("ParentPupil", b =>
                {
                    b.HasOne("SchoolHubApi.Domain.Entities.Pupil", null)
                        .WithMany()
                        .HasForeignKey("ChildrenId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SchoolHubApi.Domain.Entities.Parent", null)
                        .WithMany()
                        .HasForeignKey("ParentsId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("SchoolHubApi.Domain.Entities.Parent", b =>
                {
                    b.HasOne("SchoolHubApi.Domain.Entities.UserData", "UserData")
                        .WithMany()
                        .HasForeignKey("UserDataEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserData");
                });

            modelBuilder.Entity("SchoolHubApi.Domain.Entities.Pupil", b =>
                {
                    b.HasOne("SchoolHubApi.Domain.Entities.Classroom", "Classroom")
                        .WithMany("Pupils")
                        .HasForeignKey("ClassroomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolHubApi.Domain.Entities.UserData", "UserData")
                        .WithMany()
                        .HasForeignKey("UserDataEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Classroom");

                    b.Navigation("UserData");
                });

            modelBuilder.Entity("SchoolHubApi.Domain.Entities.ResetPasswordCode", b =>
                {
                    b.HasOne("SchoolHubApi.Domain.Entities.UserData", null)
                        .WithOne("ResetPasswordCode")
                        .HasForeignKey("SchoolHubApi.Domain.Entities.ResetPasswordCode", "Email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SchoolHubApi.Domain.Entities.Teacher", b =>
                {
                    b.HasOne("SchoolHubApi.Domain.Entities.UserData", "UserData")
                        .WithMany()
                        .HasForeignKey("UserDataEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserData");
                });

            modelBuilder.Entity("SchoolHubApi.Domain.Entities.Classroom", b =>
                {
                    b.Navigation("Pupils");
                });

            modelBuilder.Entity("SchoolHubApi.Domain.Entities.UserData", b =>
                {
                    b.Navigation("ResetPasswordCode");
                });
#pragma warning restore 612, 618
        }
    }
}
