﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TeamLeasing.DAL;
using TeamLeasing.Models;

namespace TeamLeasing.Migrations
{
    [DbContext(typeof(TeamLeasingContext))]
    partial class TeamLeasingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TeamLeasing.Models.DeveloperInProject", b =>
                {
                    b.Property<int>("DeveloperUserId");

                    b.Property<int>("ProjectId");

                    b.Property<int>("StatusForDeveloper");

                    b.HasKey("DeveloperUserId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("DeveloperInProject");
                });

            modelBuilder.Entity("TeamLeasing.Models.DeveloperUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("About");

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("Cv");

                    b.Property<int>("Experience");

                    b.Property<int>("IsFinishedUniversity");

                    b.Property<int>("Level");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Photo");

                    b.Property<string>("Province")
                        .IsRequired();

                    b.Property<string>("Surname")
                        .IsRequired();

                    b.Property<int?>("TechnologyId");

                    b.Property<string>("University");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("TechnologyId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("DeveloperUsers");
                });

            modelBuilder.Entity("TeamLeasing.Models.DeveloperUserJob", b =>
                {
                    b.Property<int>("DeveloperUserId");

                    b.Property<int>("JobId");

                    b.Property<int>("StatusForDeveloper");

                    b.HasKey("DeveloperUserId", "JobId");

                    b.HasIndex("JobId");

                    b.ToTable("DeveloperUserJob");
                });

            modelBuilder.Entity("TeamLeasing.Models.EmployeeUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("Company");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Province")
                        .IsRequired();

                    b.Property<string>("Surname")
                        .IsRequired();

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("EmployeeUsers");
                });

            modelBuilder.Entity("TeamLeasing.Models.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descritpion")
                        .IsRequired();

                    b.Property<int>("EmployeeUserId");

                    b.Property<string>("EmploymentType");

                    b.Property<bool>("IsHidden");

                    b.Property<int>("Level");

                    b.Property<int>("Price");

                    b.Property<int>("StatusForEmployee");

                    b.Property<int?>("TechnologyId");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("EmployeeUserId");

                    b.HasIndex("TechnologyId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("TeamLeasing.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime>("SendingDate");

                    b.Property<string>("Surname")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("TeamLeasing.Models.Negotiation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdditionalInformation");

                    b.Property<int>("EmploymentType");

                    b.Property<int>("OfferId");

                    b.Property<int>("Salary");

                    b.Property<int>("StatusForDeveloper");

                    b.Property<int>("StatusForEmployee");

                    b.HasKey("Id");

                    b.HasIndex("OfferId")
                        .IsUnique();

                    b.ToTable("Negotiation");
                });

            modelBuilder.Entity("TeamLeasing.Models.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdditionalInformation");

                    b.Property<decimal?>("ConstSalary");

                    b.Property<int>("DeveloperUserId");

                    b.Property<int>("EmployeeUserId");

                    b.Property<int>("EmploymentType");

                    b.Property<bool>("IsHidden");

                    b.Property<int>("Level");

                    b.Property<decimal?>("MaxSalary");

                    b.Property<decimal?>("MinSalary");

                    b.Property<int>("StatusForDeveloper");

                    b.Property<int>("StatusForEmployee");

                    b.Property<int>("TechnologyId");

                    b.HasKey("Id");

                    b.HasIndex("DeveloperUserId");

                    b.HasIndex("EmployeeUserId");

                    b.HasIndex("TechnologyId");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("TeamLeasing.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Budget");

                    b.Property<string>("Descritpion")
                        .IsRequired();

                    b.Property<int>("EmployeeUserId");

                    b.Property<DateTime>("ExecutionTime");

                    b.Property<bool>("IsHidden");

                    b.Property<int>("NumberOfDeveloperNeeded");

                    b.Property<string>("ProjectType");

                    b.Property<int>("StatusForEmployee");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int>("VacanciesRemain");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeUserId");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("TeamLeasing.Models.Technology", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Technologies");
                });

            modelBuilder.Entity("TeamLeasing.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("TeamLeasing.Models.User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("TeamLeasing.Models.User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TeamLeasing.Models.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TeamLeasing.Models.DeveloperInProject", b =>
                {
                    b.HasOne("TeamLeasing.Models.DeveloperUser", "DeveloperUser")
                        .WithMany("Projects")
                        .HasForeignKey("DeveloperUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TeamLeasing.Models.Project", "Project")
                        .WithMany("DeveloperInProject")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TeamLeasing.Models.DeveloperUser", b =>
                {
                    b.HasOne("TeamLeasing.Models.Technology", "Technology")
                        .WithMany("DeveloperUsers")
                        .HasForeignKey("TechnologyId");

                    b.HasOne("TeamLeasing.Models.User", "User")
                        .WithOne("DeveloperUser")
                        .HasForeignKey("TeamLeasing.Models.DeveloperUser", "UserId");
                });

            modelBuilder.Entity("TeamLeasing.Models.DeveloperUserJob", b =>
                {
                    b.HasOne("TeamLeasing.Models.DeveloperUser", "DeveloperUser")
                        .WithMany("Jobs")
                        .HasForeignKey("DeveloperUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TeamLeasing.Models.Job", "Job")
                        .WithMany("DeveloperUsers")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TeamLeasing.Models.EmployeeUser", b =>
                {
                    b.HasOne("TeamLeasing.Models.User", "User")
                        .WithOne("EmployeeUser")
                        .HasForeignKey("TeamLeasing.Models.EmployeeUser", "UserId");
                });

            modelBuilder.Entity("TeamLeasing.Models.Job", b =>
                {
                    b.HasOne("TeamLeasing.Models.EmployeeUser", "EmployeeUser")
                        .WithMany("Jobs")
                        .HasForeignKey("EmployeeUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TeamLeasing.Models.Technology", "Technology")
                        .WithMany("Jobs")
                        .HasForeignKey("TechnologyId");
                });

            modelBuilder.Entity("TeamLeasing.Models.Negotiation", b =>
                {
                    b.HasOne("TeamLeasing.Models.Offer", "Offer")
                        .WithOne("Negotiation")
                        .HasForeignKey("TeamLeasing.Models.Negotiation", "OfferId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TeamLeasing.Models.Offer", b =>
                {
                    b.HasOne("TeamLeasing.Models.DeveloperUser", "DeveloperUser")
                        .WithMany("Offers")
                        .HasForeignKey("DeveloperUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TeamLeasing.Models.EmployeeUser", "EmployeeUser")
                        .WithMany("Offers")
                        .HasForeignKey("EmployeeUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TeamLeasing.Models.Technology", "Technology")
                        .WithMany()
                        .HasForeignKey("TechnologyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TeamLeasing.Models.Project", b =>
                {
                    b.HasOne("TeamLeasing.Models.EmployeeUser", "EmployeeUser")
                        .WithMany("Projects")
                        .HasForeignKey("EmployeeUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
