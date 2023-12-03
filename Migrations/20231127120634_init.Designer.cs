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
    [Migration("20231127120634_init")]
    partial class init
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

            modelBuilder.Entity("SchoolHubApi.Domain.Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClassroomId")
                        .HasColumnType("int");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClassroomId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("SchoolHubApi.Domain.Entities.Homework", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("HomeworkFile")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("HomeworkFileType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PupilId")
                        .HasColumnType("int");

                    b.Property<int>("TopicId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PupilId");

                    b.HasIndex("TopicId");

                    b.ToTable("Homeworks");
                });

            modelBuilder.Entity("SchoolHubApi.Domain.Entities.Mark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("HomeworkId")
                        .HasColumnType("int");

                    b.Property<int>("MarkName")
                        .HasColumnType("int");

                    b.Property<int>("PupilId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HomeworkId")
                        .IsUnique();

                    b.HasIndex("PupilId");

                    b.ToTable("Marks");
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

                    b.HasIndex("UserDataEmail")
                        .IsUnique();

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

                    b.HasIndex("UserDataEmail")
                        .IsUnique();

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

                    b.Property<byte[]>("TeacherPlan")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("TeacherPlanContentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserDataEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserDataEmail")
                        .IsUnique();

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("SchoolHubApi.Domain.Entities.Topic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("TopicFile")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("TopicFileType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TopicName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Topics");
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
                            PasswordHash = new byte[] { 210, 177, 254, 227, 76, 120, 254, 33, 244, 196, 239, 191, 187, 101, 126, 11, 67, 63, 190, 81, 44, 30, 128, 44, 149, 5, 33, 198, 212, 132, 23, 208, 45, 1, 229, 127, 198, 5, 177, 198, 101, 52, 35, 22, 216, 41, 212, 100, 236, 63, 11, 250, 219, 73, 47, 187, 7, 85, 160, 159, 213, 146, 114, 135 },
                            PasswordSalt = new byte[] { 82, 44, 14, 238, 175, 156, 165, 198, 160, 225, 88, 205, 183, 29, 90, 88, 121, 28, 83, 88, 148, 169, 18, 232, 150, 142, 173, 65, 137, 73, 207, 10, 255, 150, 186, 124, 48, 107, 137, 243, 232, 225, 21, 7, 75, 212, 225, 215, 33, 203, 54, 146, 85, 244, 64, 212, 254, 178, 180, 187, 13, 87, 138, 163, 210, 84, 82, 162, 13, 158, 210, 49, 127, 232, 136, 241, 210, 54, 8, 186, 78, 34, 229, 37, 20, 240, 61, 31, 153, 14, 4, 112, 157, 217, 106, 91, 117, 140, 30, 96, 191, 23, 105, 186, 163, 59, 46, 116, 5, 188, 167, 151, 231, 206, 28, 65, 118, 99, 9, 74, 129, 96, 42, 252, 71, 18, 75, 205 },
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

            modelBuilder.Entity("SchoolHubApi.Domain.Entities.Course", b =>
                {
                    b.HasOne("SchoolHubApi.Domain.Entities.Classroom", "Classroom")
                        .WithMany("Courses")
                        .HasForeignKey("ClassroomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SchoolHubApi.Domain.Entities.Teacher", "Teacher")
                        .WithMany("Courses")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Classroom");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("SchoolHubApi.Domain.Entities.Homework", b =>
                {
                    b.HasOne("SchoolHubApi.Domain.Entities.Pupil", "Pupil")
                        .WithMany("Homeworks")
                        .HasForeignKey("PupilId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SchoolHubApi.Domain.Entities.Topic", "Topic")
                        .WithMany("Homeworks")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Pupil");

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("SchoolHubApi.Domain.Entities.Mark", b =>
                {
                    b.HasOne("SchoolHubApi.Domain.Entities.Homework", "Homework")
                        .WithOne("Mark")
                        .HasForeignKey("SchoolHubApi.Domain.Entities.Mark", "HomeworkId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SchoolHubApi.Domain.Entities.Pupil", "Pupil")
                        .WithMany("Marks")
                        .HasForeignKey("PupilId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Homework");

                    b.Navigation("Pupil");
                });

            modelBuilder.Entity("SchoolHubApi.Domain.Entities.Parent", b =>
                {
                    b.HasOne("SchoolHubApi.Domain.Entities.UserData", "UserData")
                        .WithOne("Parent")
                        .HasForeignKey("SchoolHubApi.Domain.Entities.Parent", "UserDataEmail")
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
                        .WithOne("Pupil")
                        .HasForeignKey("SchoolHubApi.Domain.Entities.Pupil", "UserDataEmail")
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
                        .WithOne("Teacher")
                        .HasForeignKey("SchoolHubApi.Domain.Entities.Teacher", "UserDataEmail")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserData");
                });

            modelBuilder.Entity("SchoolHubApi.Domain.Entities.Topic", b =>
                {
                    b.HasOne("SchoolHubApi.Domain.Entities.Course", "Course")
                        .WithMany("Topic")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("SchoolHubApi.Domain.Entities.Classroom", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Pupils");
                });

            modelBuilder.Entity("SchoolHubApi.Domain.Entities.Course", b =>
                {
                    b.Navigation("Topic");
                });

            modelBuilder.Entity("SchoolHubApi.Domain.Entities.Homework", b =>
                {
                    b.Navigation("Mark")
                        .IsRequired();
                });

            modelBuilder.Entity("SchoolHubApi.Domain.Entities.Pupil", b =>
                {
                    b.Navigation("Homeworks");

                    b.Navigation("Marks");
                });

            modelBuilder.Entity("SchoolHubApi.Domain.Entities.Teacher", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("SchoolHubApi.Domain.Entities.Topic", b =>
                {
                    b.Navigation("Homeworks");
                });

            modelBuilder.Entity("SchoolHubApi.Domain.Entities.UserData", b =>
                {
                    b.Navigation("Parent");

                    b.Navigation("Pupil");

                    b.Navigation("ResetPasswordCode");

                    b.Navigation("Teacher");
                });
#pragma warning restore 612, 618
        }
    }
}