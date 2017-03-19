using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TeamLeasing.DAL;

namespace TeamLeasing.Migrations
{
    [DbContext(typeof(TeamLeasingContext))]
    [Migration("20170319180104_Initialdatabase")]
    partial class Initialdatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TeamLeasing.Models.Developer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("Cv");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<int>("Experience");

                    b.Property<string>("Level");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Photo");

                    b.Property<string>("Province")
                        .IsRequired();

                    b.Property<string>("Surname")
                        .IsRequired();

                    b.Property<int?>("TechnologyId");

                    b.Property<string>("University");

                    b.HasKey("Id");

                    b.HasIndex("TechnologyId");

                    b.ToTable("Developers");
                });

            modelBuilder.Entity("TeamLeasing.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Phone");

                    b.Property<string>("Province")
                        .IsRequired();

                    b.Property<string>("Surname")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("TeamLeasing.Models.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Descritpion")
                        .IsRequired();

                    b.Property<int?>("EmployeeId");

                    b.Property<int>("Price");

                    b.Property<string>("Status");

                    b.Property<string>("Technology");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

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

                    b.Property<string>("Surname")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("TeamLeasing.Models.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DeveloperId");

                    b.Property<int?>("EmployeeId");

                    b.Property<string>("Level")
                        .IsRequired();

                    b.Property<decimal>("Price");

                    b.Property<string>("Status")
                        .IsRequired();

                    b.Property<string>("Technology")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("DeveloperId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("TeamLeasing.Models.Technology", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Technologies");
                });

            modelBuilder.Entity("TeamLeasing.Models.Developer", b =>
                {
                    b.HasOne("TeamLeasing.Models.Technology", "Technology")
                        .WithMany("Developers")
                        .HasForeignKey("TechnologyId");
                });

            modelBuilder.Entity("TeamLeasing.Models.Job", b =>
                {
                    b.HasOne("TeamLeasing.Models.Employee")
                        .WithMany("Jobs")
                        .HasForeignKey("EmployeeId");
                });

            modelBuilder.Entity("TeamLeasing.Models.Offer", b =>
                {
                    b.HasOne("TeamLeasing.Models.Developer", "Developer")
                        .WithMany("Offers")
                        .HasForeignKey("DeveloperId");

                    b.HasOne("TeamLeasing.Models.Employee", "Employee")
                        .WithMany("Offers")
                        .HasForeignKey("EmployeeId");
                });
        }
    }
}
