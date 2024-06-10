using ColonySimulator.Backend.Helpers;
using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Persistence.Enums;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ColonySimulator.Backend.Services;

/// <summary>
/// Manages entities in simulation
/// </summary>
public class EntityManagementService : IEntityManagementService
{
    private readonly IServiceScopeFactory _serviceScope;
    private readonly PopCounter _counter;
    
    /// <summary>
    /// Constructor for service
    /// </summary>
    /// <param name="serviceScope">Service scope</param>
    /// <param name="counter">Population counter</param>
    public EntityManagementService(IServiceScopeFactory serviceScope, PopCounter counter)
    {
        _serviceScope = serviceScope;
        _counter = counter;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="summaricCount">Summaric count of entities</param>
    /// <param name="ct"></param>
    public async Task GenerateNewEntity(int summaricCount, CancellationToken ct)
    {
        if (summaricCount >= 50)
        {
            using var scope = _serviceScope.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<ColonySimulatorContext>();
            var rand = new Random();

            int quantity = rand.Next(1, 5);
            _counter.PopulationCount += quantity;
            
            var entityList = new List<Proffesion>
            {
                new Apothecary
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
                },
                new BlackSmith
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
                },
                new Farmer
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
                },
                new Medic
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
                },
                new Timber
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
                }
            };

            if (dbContext is not null)
            {
                for (int i = 0; i < quantity; i++)
                {
                    var id = rand.Next(0, 1);

                    if (entityList[id].GetType() == typeof(Apothecary))
                    {
                        await dbContext.Apothecaries.AddAsync((Apothecary)entityList[id], ct);
                    }
                    else if(entityList[id].GetType() == typeof(BlackSmith))
                    {
                        await dbContext.BlackSmiths.AddAsync((BlackSmith)entityList[id], ct);
                    }
                    else if (entityList[id].GetType() == typeof(Farmer))
                    {
                        await dbContext.Farmers.AddAsync((Farmer)entityList[id], ct);
                    }
                    else if (entityList[id].GetType() == typeof(Medic))
                    {
                        await dbContext.Medics.AddAsync((Medic)entityList[id], ct);
                    }
                    else if (entityList[id].GetType() == typeof(Timber))
                    {
                        await dbContext.Timbers.AddAsync((Timber)entityList[id], ct);
                    }
                }
            }
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ct"></param>
    public async Task CleanupDeadEntities(CancellationToken ct)
    {
        using var scope = _serviceScope.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<ColonySimulatorContext>();

        if (dbContext is not null)
        {
            List<Proffesion> professions = new();
            professions.AddRange(await dbContext.Apothecaries.Where(x => x.Vitality == 0).ToListAsync(ct));
            professions.AddRange(await dbContext.Farmers.Where(x => x.Vitality == 0).ToListAsync(ct));
            professions.AddRange(await dbContext.BlackSmiths.Where(x => x.Vitality == 0).ToListAsync(ct));
            professions.AddRange(await dbContext.Medics.Where(x => x.Vitality == 0).ToListAsync(ct));
            professions.AddRange(await dbContext.Timbers.Where(x => x.Vitality == 0).ToListAsync(ct));

            _counter.PopulationCount -= professions.Count;
            
            dbContext.RemoveRange(professions);
            await dbContext.SaveChangesAsync(ct);
        }
    }
    
    
    /// <summary>
    /// Handle sick status
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    public async Task CheckSickStatus(CancellationToken ct)
    {
        using var scope = _serviceScope.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<ColonySimulatorContext>();

        if (dbContext is not null)
        {
            var sickPeople = new List<Person>();
            sickPeople.AddRange(await dbContext.Medics.Where(x => x.IsSick == true).ToListAsync(ct));
            sickPeople.AddRange(await dbContext.Timbers.Where(x => x.IsSick == true).ToListAsync(ct));
            sickPeople.AddRange(await dbContext.Farmers.Where(x => x.IsSick == true).ToListAsync(ct));
            sickPeople.AddRange(await dbContext.BlackSmiths.Where(x => x.IsSick == true).ToListAsync(ct));
            sickPeople.AddRange(await dbContext.Apothecaries.Where(x => x.IsSick == true).ToListAsync(ct));
            sickPeople.AddRange(await dbContext.Traders.Where(x => x.IsSick == true).ToListAsync(ct));


            foreach (var item in sickPeople)
            {
                item.Vitality -= 1;
            }

            await dbContext.SaveChangesAsync(ct);
        }
    }
    
    /// <summary>
    /// Handle hunger status
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    public async Task CheckHungerStatus(CancellationToken ct)
    {
        using var scope = _serviceScope.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<ColonySimulatorContext>();

        if (dbContext is not null)
        {
            var hungryPeople = new List<Person>();
            hungryPeople.AddRange(await dbContext.Medics.Where(x => x.IsHungry == true).ToListAsync(ct));
            hungryPeople.AddRange(await dbContext.Timbers.Where(x => x.IsHungry == true).ToListAsync(ct));
            hungryPeople.AddRange(await dbContext.Farmers.Where(x => x.IsHungry == true).ToListAsync(ct));
            hungryPeople.AddRange(await dbContext.BlackSmiths.Where(x => x.IsHungry == true).ToListAsync(ct));
            hungryPeople.AddRange(await dbContext.Apothecaries.Where(x => x.IsHungry == true).ToListAsync(ct));
            hungryPeople.AddRange(await dbContext.Traders.Where(x => x.IsHungry == true).ToListAsync(ct));

            foreach (var item in hungryPeople)
            {
                item.Vitality -= 1;
            }

            if (dbContext.Crops.SingleOrDefault(x => x.Id == 1)!.CropsCount >= _counter.PopulationCount)
            {
                foreach (var item in hungryPeople)
                {
                    item.IsHungry = false;
                }
            }

            await dbContext.SaveChangesAsync(ct);
        }
    }
}