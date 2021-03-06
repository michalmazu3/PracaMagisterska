﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TeamLeasing.Models;

namespace TeamLeasing.DAL
{
    public class TeamLeasingContext : IdentityDbContext<User>
    {
        public IConfigurationRoot _config;

        public TeamLeasingContext(IConfigurationRoot config, DbContextOptions options) : base(options)
        {
            _config = config;
        }

        public DbSet<DeveloperUser> DeveloperUsers { get; set; }
        public DbSet<EmployeeUser> EmployeeUsers { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Negotiation> Negotiation { get; set; }

        public DbSet<Message> Messages { get; set; }
        public DbSet<DeveloperUserJob> DeveloperUserJob { get; set; }

        public DbSet<DeveloperInProject> DeveloperInProject { get; set; }
        public DbSet<Project> Project { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:TeamLeasingConnectionStringAzure"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Offer>().HasOne(o => o.Negotiation)
                .WithOne(w => w.Offer).HasForeignKey<Negotiation>(n => n.OfferId);

            modelBuilder.Entity<User>().HasOne(h => h.DeveloperUser).WithOne(w => w.User)
                .HasForeignKey<DeveloperUser>(h => h.UserId);

            modelBuilder.Entity<User>().HasOne(h => h.EmployeeUser).WithOne(w => w.User)
                .HasForeignKey<EmployeeUser>(h => h.UserId);

            modelBuilder.Entity<DeveloperUserJob>()
                .HasKey(d => new {d.DeveloperUserId, d.JobId});

            modelBuilder.Entity<DeveloperUserJob>()
                .HasOne(o => o.DeveloperUser)
                .WithMany(m => m.Jobs)
                .HasForeignKey(f => f.DeveloperUserId);

            modelBuilder.Entity<DeveloperUserJob>()
                .HasOne(o => o.Job)
                .WithMany(w => w.DeveloperUsers)
                .HasForeignKey(f => f.JobId);

            modelBuilder.Entity<Offer>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<Offer>()
                .HasOne(o => o.DeveloperUser)
                .WithMany(m => m.Offers)
                .HasForeignKey(f => f.DeveloperUserId);

            modelBuilder.Entity<Offer>()
                .HasOne(o => o.EmployeeUser)
                .WithMany(m => m.Offers)
                .HasForeignKey(f => f.EmployeeUserId);



            modelBuilder.Entity<DeveloperInProject>()
                .HasKey(d => new { d.DeveloperUserId, d.ProjectId});

            modelBuilder.Entity<DeveloperInProject>()
                .HasOne(o => o.DeveloperUser)
                .WithMany(m => m.Projects)
                .HasForeignKey(f => f.DeveloperUserId);

            modelBuilder.Entity<DeveloperInProject>()
                .HasOne(o => o.Project)
                .WithMany(w => w.DeveloperInProject)
                .HasForeignKey(f => f.ProjectId);


            base.OnModelCreating(modelBuilder);
        }
    }
}