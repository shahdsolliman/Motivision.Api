using Microsoft.EntityFrameworkCore;
using Motivision.Core.Business.Entities;
using Motivision.Core.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Infrastructure.Persistence
{
    public class AppBusinessDbContext : DbContext
    {
        public AppBusinessDbContext(DbContextOptions<AppBusinessDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<AppUser>();
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastUpdated = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<FocusSession> FocusSessions { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<GoalStep> Steps { get; set; }

    }
}
