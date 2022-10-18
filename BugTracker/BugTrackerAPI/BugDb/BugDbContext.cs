using BugTrackerModel;
using Microsoft.EntityFrameworkCore;

namespace BugTrackerAPI.BugDb
{
    public class BugDbContext : DbContext
    {
        public BugDbContext(DbContextOptions<BugDbContext> options) : base(options)
        { }
        public DbSet<Bug> Bugs { get; set; }
        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bug>().ToTable("Bug");
           //     .HasOne<Person>(b => b.Assignee);
            modelBuilder.Entity<Person>().ToTable("Person");
        }

    }
}
