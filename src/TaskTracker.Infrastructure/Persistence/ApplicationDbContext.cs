using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TaskTracker.Domain;

namespace TaskTracker.Infrastructure.Persistence
{
    public class ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> dbContext)
        : DbContext(dbContext)
    {
        private readonly DbContextOptions<ApplicationDbContext> _dbContext = dbContext;

        public DbSet<TaskItem> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}