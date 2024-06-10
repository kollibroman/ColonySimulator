using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;
using ColonySimulator.Backend.Persistence.Models.Threats;
using Microsoft.EntityFrameworkCore;

namespace ColonySimulator.Backend.Persistence;


/// <summary>
/// Db context to operate with database linked to application
/// </summary>
public class ColonySimulatorContext : DbContext
{
    /// <summary>
    /// Constructor with options
    /// </summary>
    /// <param name="options">dbContext options</param>
    public ColonySimulatorContext(DbContextOptions<ColonySimulatorContext> options) : base(options)
    {
    }

    public virtual DbSet<Apothecary> Apothecaries => Set<Apothecary>();
    public virtual DbSet<BlackSmith> BlackSmiths => Set<BlackSmith>();
    public virtual DbSet<Farmer> Farmers => Set<Farmer>();
    public virtual DbSet<Medic> Medics => Set<Medic>();
    public virtual DbSet<Timber> Timbers => Set<Timber>();
    public virtual DbSet<Trader> Traders => Set<Trader>();
    public virtual DbSet<Crops> Crops => Set<Crops>();
    public virtual DbSet<Medicine> Medicines => Set<Medicine>();
    public virtual DbSet<Weaponry> Weaponry => Set<Weaponry>();
    public virtual DbSet<Wood> Wood => Set<Wood>();
    public virtual DbSet<Herbs> Herbs => Set<Herbs>();
    public virtual DbSet<FightingThreat?> FightingThreats => Set<FightingThreat>();
    public virtual DbSet<NaturalThreat> NaturalThreats => Set<NaturalThreat>();
    public virtual DbSet<PlagueThreat> PlagueThreats => Set<PlagueThreat>();

    //for testing purposes only, don't seed with other data than test 
    public virtual DbSet<Proffesion> Proffesions => Set<Proffesion>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ColonySimulatorContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}