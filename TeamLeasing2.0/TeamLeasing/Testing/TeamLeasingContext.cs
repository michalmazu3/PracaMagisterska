using Microsoft.EntityFrameworkCore;
using TeamLeasing.Models;

namespace Testing
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions options)
            : base(options)
        {
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


    }
}