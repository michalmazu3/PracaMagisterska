using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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

        public DbSet<Developer> Developers { get; set; }
        public DbSet<DeveloperUser> DeveloperUsers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Message> Messages { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:TeamLeasingConnectionString"]);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().
                HasOne(h => h.DeveloperUser).
                WithOne(w => w.User).
                HasForeignKey<DeveloperUser>(h => h.UserId);

        
            base.OnModelCreating(modelBuilder);

        }
    }
}