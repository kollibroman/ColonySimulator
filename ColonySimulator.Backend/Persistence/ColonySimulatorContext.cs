using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;
using ColonySimulator.Backend.Persistence.Models.Threats;
using Microsoft.EntityFrameworkCore;

namespace ColonySimulator.Backend.Persistence;

public class ColonySimulatorContext : DbContext
{
    public ColonySimulatorContext(DbContextOptions<ColonySimulatorContext> options) : base(options)
    {
    }

    public DbSet<Apothecary> Apothecaries => Set<Apothecary>();
    public DbSet<BlackSmith> BlackSmiths => Set<BlackSmith>();
    public DbSet<Farmer> Farmers => Set<Farmer>();
    public DbSet<Medic> Medics => Set<Medic>();
    public DbSet<Timber> Timbers => Set<Timber>();
    public DbSet<Trader> Traders => Set<Trader>();
    public DbSet<Crops> Crops => Set<Crops>();
    public DbSet<Medicine> Medicines => Set<Medicine>();
    public DbSet<Weaponry> Weaponry => Set<Weaponry>();
    public DbSet<Wood> Wood => Set<Wood>();
    public DbSet<Herbs> Herbs => Set<Herbs>();
    public DbSet<FightingThreat> FightingThreats => Set<FightingThreat>();
    public DbSet<NaturalThreat> NaturalThreats => Set<NaturalThreat>();
    public DbSet<PlagueThreat> PlagueThreats => Set<PlagueThreat>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ColonySimulatorContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}