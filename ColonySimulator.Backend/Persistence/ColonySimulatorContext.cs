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
    
    /// <summary>
    /// Apothecaries DbSet
    /// </summary>
    public virtual DbSet<Apothecary> Apothecaries => Set<Apothecary>();
    
    /// <summary>
    /// BlackSmiths DbSet
    /// </summary>
    public virtual DbSet<BlackSmith> BlackSmiths => Set<BlackSmith>();
    
    /// <summary>
    /// Farmers DbSet
    /// </summary>
    public virtual DbSet<Farmer> Farmers => Set<Farmer>();
    
    /// <summary>
    /// Medics DbSet
    /// </summary>
    public virtual DbSet<Medic> Medics => Set<Medic>();
    
    /// <summary>
    /// Timbers DbSet
    /// </summary>
    public virtual DbSet<Timber> Timbers => Set<Timber>();
    
    /// <summary>
    /// Trader DbSet
    /// </summary>
    public virtual DbSet<Trader> Traders => Set<Trader>();
    
    /// <summary>
    /// Crops DbSet
    /// </summary>
    public virtual DbSet<Crops> Crops => Set<Crops>();
    
    /// <summary>
    /// Medicines DbSet
    /// </summary>
    public virtual DbSet<Medicine> Medicines => Set<Medicine>();
    
    /// <summary>
    /// Weaponry DbSet
    /// </summary>
    public virtual DbSet<Weaponry> Weaponry => Set<Weaponry>();
    
    /// <summary>
    /// Wood DbSet
    /// </summary>
    public virtual DbSet<Wood> Wood => Set<Wood>();
    
    /// <summary>
    /// Herbs DbSet
    /// </summary>
    public virtual DbSet<Herbs> Herbs => Set<Herbs>();
    
    /// <summary>
    /// Fighting threats DbSet
    /// </summary>
    public virtual DbSet<FightingThreat> FightingThreats => Set<FightingThreat>();
    
    /// <summary>
    /// Natural threats DbSet
    /// </summary>
    public virtual DbSet<NaturalThreat> NaturalThreats => Set<NaturalThreat>();
    
    /// <summary>
    /// Plague threats DbSet
    /// </summary>
    public virtual DbSet<PlagueThreat> PlagueThreats => Set<PlagueThreat>();

    //for testing purposes only, don't seed with other data than test 
    /// <summary>
    /// Proffesions DbSet
    /// </summary>
    public virtual DbSet<Proffesion> Proffesions => Set<Proffesion>();
    
    /// <summary>
    /// On model creating method
    /// </summary>
    /// <param name="modelBuilder">ModelBuilder class</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ColonySimulatorContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}