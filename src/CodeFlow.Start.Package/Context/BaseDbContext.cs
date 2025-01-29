using CodeFlow.Start.Package.Context.Configurations;
using CodeFlow.Start.Package.Context.Tracking;
using Microsoft.EntityFrameworkCore;

namespace CodeFlow.Start.Package.Context;

public class BaseDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<CommandFailure> CommandFailures { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new CommandFailureConfiguration());
    }
}
