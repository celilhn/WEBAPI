using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class TraDbContext : DbContext
    {
        public TraDbContext(DbContextOptions<TraDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(b => b.InsertionDate).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<User>().Property(b => b.UpdateDate).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<User>().Property(b => b.Status).HasDefaultValue(1);
        }
    }
}
