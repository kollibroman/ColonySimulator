using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ColonySimulator.persistence;

public class ColonySimulatorContext : DbContext
{
    public ColonySimulatorContext(DbContextOptions<ColonySimulatorContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ColonySimulatorContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}