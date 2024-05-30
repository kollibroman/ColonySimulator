using ColonySimulator.Backend.Helpers;
using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Persistence.Enums;
using ColonySimulator.Backend.Persistence.Models.Professions;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ColonySimulator.Backend.Seeders;
/// <summary>
/// Data seeder for profession objects
/// </summary>
public class ProfessionSeeder
{
    private readonly ColonySimulatorContext _dbContext;
    private readonly ILogger _logger;
    private readonly PopCounter _counter;
    
    /// <summary>
    /// Constructor for it with DI parameters
    /// </summary>
    /// <param name="dbContext">Db context class</param>
    /// <param name="logger">Logger to log data</param>
    /// <param name="counter">Population counter class</param>
    public ProfessionSeeder(ColonySimulatorContext dbContext, ILogger logger, PopCounter counter) 
    {
        _dbContext = dbContext;
        _logger = logger;
        _counter = counter;
    }
    
    /// <summary>
    /// Seeds apothecary object
    /// </summary>
    /// <param name="apothecaryCount">Number of Apothecaries</param>
    /// <param name="ct">Cancellation token</param>
    
    public async Task SeedApothecary(int apothecaryCount, CancellationToken ct)
    {
        _logger.Information("Seeding apothecaries...");
        var rand = new Random();
        var entityList = new List<Apothecary>();
        
        for (int i = 0; i <= apothecaryCount ; i++)
        {
            var entity = new Apothecary
            {
                Agility = 1,
                ApothecaryLevel = rand.Next(1, 4),
                Gender = (Gender)rand.Next(0, 1),
                RequiredAgility = 1,
                RequiredStrength = 1,
                Strength = 1,
                Vitality = rand.Next(1, 10),
                ResourceConsumption = rand.Next(1, 10),
                IsSick = false,
                IsHungry = false
            };
            
            entityList.Add(entity);
        }

        _counter.PopulationCount += apothecaryCount;
        await _dbContext.AddRangeAsync(entityList, ct);
        await _dbContext.SaveChangesAsync(ct);
    }
    
    /// <summary>
    /// Seed blacksmiths
    /// </summary>
    /// <param name="blacksmithCount">number of blacksmiths to seed</param>
    /// <param name="ct">Cancellation token</param>
    
    public async Task SeedBlacksmith(int blacksmithCount, CancellationToken ct)
    {
        _logger.Information("Seeding blacksmiths...");
        var rand = new Random();
        var entityList = new List<BlackSmith>();
        
        for (int i = 0; i <= blacksmithCount ; i++)
        {
            var entity = new BlackSmith
            {
                Agility = 2,
                BlackSmithLevel = rand.Next(1, 5),
                Gender = (Gender)rand.Next(0, 1),
                RequiredAgility = 2,
                RequiredStrength = 4,
                Strength = 4,
                Vitality = rand.Next(1, 10),
                ResourceConsumption = rand.Next(1, 10),
                IsSick = false,
                IsHungry = false
            };
            
            entityList.Add(entity);
        }

        _counter.PopulationCount += blacksmithCount;
        await _dbContext.BlackSmiths.AddRangeAsync(entityList, ct);
        await _dbContext.SaveChangesAsync(ct);
    }
    
    /// <summary>
    /// Farmer seeder
    /// </summary>
    /// <param name="farmerCount">number of farmers to seed</param>
    /// <param name="ct">Cancellation token</param>
    
    public async Task SeedFarmer(int farmerCount, CancellationToken ct)
    {
        _logger.Information("Seeding farmers...");
        var rand = new Random();
        var entityList = new List<Farmer>();
        
        for (int i = 0; i <= farmerCount ; i++)
        {
            var entity = new Farmer
            {
                Agility = 3,
                FarmingLevel = rand.Next(1, 5),
                Gender = (Gender)rand.Next(0, 1),
                RequiredAgility = 3,
                RequiredStrength = 5,
                Strength = 5,
                Vitality = rand.Next(1, 10),
                ResourceConsumption = rand.Next(1, 10),
                IsSick = false,
                IsHungry = false
            };
            
            entityList.Add(entity);
        }

        _counter.PopulationCount += farmerCount;
        await _dbContext.Farmers.AddRangeAsync(entityList, ct);
        await _dbContext.SaveChangesAsync(ct);
    }
    
    /// <summary>
    /// Seed medics
    /// </summary>
    /// <param name="medicCount">Number of medics</param>
    /// <param name="ct">Cancellation token</param>
    
    public async Task SeedMedic(int medicCount, CancellationToken ct)
    {
        _logger.Information("Seeding medics...");
        var rand = new Random();
        var entityList = new List<Medic>();
        
        for (int i = 0; i <= medicCount ; i++)
        {
            var entity = new Medic
            {
                Agility = 2,
                MedicLevel = rand.Next(1, 5),
                Gender = (Gender)rand.Next(0, 1),
                RequiredAgility = 2,
                RequiredStrength = 1,
                Strength = 1,
                Vitality = rand.Next(1, 10),
                ResourceConsumption = rand.Next(1, 10),
                IsSick = false,
                IsHungry = false
            };
            
            entityList.Add(entity);
        }

        _counter.PopulationCount += medicCount;
        await _dbContext.Medics.AddRangeAsync(entityList, ct);
        await _dbContext.SaveChangesAsync(ct);
    }
    
    /// <summary>
    /// Seed timbers
    /// </summary>
    /// <param name="timbersCount">Number of seeders to seed</param>
    /// <param name="ct">Cancellation token</param>
    
    public async Task SeedTimbers(int timbersCount, CancellationToken ct)
    {
        _logger.Information("Seeding timbers...");
        var rand = new Random();
        var entityList = new List<Timber>();
        
        for (int i = 0; i <= timbersCount ; i++)
        {
            var entity = new Timber
            {
                Agility = 4,
                TimberLevel = rand.Next(1, 5),
                Gender = (Gender)rand.Next(0, 1),
                RequiredAgility = 4,
                RequiredStrength = 5,
                Strength = 5,
                Vitality = rand.Next(1, 10),
                ResourceConsumption = rand.Next(1, 10),
                IsSick = false,
                IsHungry = false
            };
            
            entityList.Add(entity);
        }

        _counter.PopulationCount += timbersCount;
        await _dbContext.Timbers.AddRangeAsync(entityList, ct);
        await _dbContext.SaveChangesAsync(ct);
    }
    
    /// <summary>
    /// Seed traders
    /// </summary>
    /// <param name="traderCount">Number of traders to seed</param>
    /// <param name="ct">Cancellation token</param>
    
    public async Task SeedTraders(int traderCount, CancellationToken ct)
    {
        //Changed the seeder to create only one trader
        _logger.Information("Seeding traders...");
        var rand = new Random();
        
        var entity = new Trader
        {
            Agility = 5,
            TradingLevel = rand.Next(1, 5),
            Gender = (Gender)rand.Next(0, 1),
            RequiredAgility = 5,
            RequiredStrength = 1,
            Strength = 1,
            Vitality = rand.Next(1, 10),
            ResourceConsumption = rand.Next(1, 10),
            IsSick = false,
            IsHungry = false
        };
        

        _counter.PopulationCount += 1;
        await _dbContext.Traders.AddAsync(entity , ct);
        await _dbContext.SaveChangesAsync(ct);
    }
}