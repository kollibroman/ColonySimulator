using ColonySimulator.Backend.Helpers;
using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Persistence.Enums;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Threats;
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
    /// <param name="cropsCount">Crops count of entities</param>
    /// <param name="ct"></param>
    public async Task GenerateNewEntity(int cropsCount, CancellationToken ct)
    {
        if (cropsCount >= 10)
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
                    var id = rand.Next(0, 5);

                    if (entityList[id].GetType() == typeof(Apothecary))
                    {
                        await dbContext.Apothecaries.AddAsync((Apothecary)entityList[id], ct);
                        _counter.ApothecariesCount++;
                    }
                    else if(entityList[id].GetType() == typeof(BlackSmith))
                    {
                        await dbContext.BlackSmiths.AddAsync((BlackSmith)entityList[id], ct);
                        _counter.BlackSmithCount++;
                    }
                    else if (entityList[id].GetType() == typeof(Farmer))
                    {
                        await dbContext.Farmers.AddAsync((Farmer)entityList[id], ct);
                        _counter.FarmerCount++;
                    }
                    else if (entityList[id].GetType() == typeof(Medic))
                    {
                        await dbContext.Medics.AddAsync((Medic)entityList[id], ct);
                        _counter.MedicCount++;
                    }
                    else if (entityList[id].GetType() == typeof(Timber))
                    {
                        await dbContext.Timbers.AddAsync((Timber)entityList[id], ct);
                        _counter.TimberCount++;
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
            var apothecaries = await dbContext.Apothecaries.Where(x => x.Vitality <= 0).ToListAsync(ct);
            var farmers = await dbContext.Farmers.Where(x => x.Vitality <= 0).ToListAsync(ct);
            var blacksmiths = await dbContext.BlackSmiths.Where(x => x.Vitality <= 0).ToListAsync(ct);
            var medics = await dbContext.Medics.Where(x => x.Vitality <= 0).ToListAsync(ct);
            var timbers = await dbContext.Timbers.Where(x => x.Vitality <= 0).ToListAsync(ct);

            List<int> professionCount =
            [
                apothecaries.Count,
                farmers.Count,
                blacksmiths.Count,
                medics.Count,
                timbers.Count
            ];

            if (_counter.ApothecariesCount - professionCount[0] >= 0)
            {
                _counter.PeopleLost++;
                _counter.ApothecariesCount -= professionCount[0];
            }
            else
            {
                _counter.PeopleLost++;
                _counter.ApothecariesCount = 0;
            }

            if (_counter.BlackSmithCount - professionCount[2] >= 0)
            {
                _counter.PeopleLost++;
                _counter.BlackSmithCount -= professionCount[2];
            }
            else
            {
                _counter.PeopleLost++;
                _counter.BlackSmithCount = 0;
            }

            if (_counter.FarmerCount - professionCount[1] >= 0)
            {
                _counter.PeopleLost++;
                _counter.FarmerCount -= professionCount[1];
            }
            else
            {
                _counter.PeopleLost++;
                _counter.FarmerCount = 0;    
            }
            
            if(_counter.MedicCount - professionCount[3] >= 0)
            {
                _counter.PeopleLost++;
                _counter.MedicCount -= professionCount[3];
            }
            else
            {
                _counter.PeopleLost++;
                _counter.MedicCount = 0;
            }
            
            if (_counter.TimberCount - professionCount[4] >= 0)
            {
                _counter.PeopleLost++;
                _counter.TimberCount -= professionCount[4];
            }
            else
            {
                _counter.PeopleLost++;
                _counter.TimberCount = 0;
            }
            
            dbContext.Apothecaries.RemoveRange(apothecaries);
            dbContext.Farmers.RemoveRange(farmers);
            dbContext.BlackSmiths.RemoveRange(blacksmiths);
            dbContext.Medics.RemoveRange(medics);
            dbContext.Timbers.RemoveRange(timbers);
            
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
                item.Vitality -= 2;
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
            if (dbContext.Crops.SingleOrDefault(x => x.Id == 1)!.CropsCount == 0)
            {
                var people = new List<Person>();
                people.AddRange(await dbContext.Medics.ToListAsync(ct));
                people.AddRange(await dbContext.Timbers.ToListAsync(ct));
                people.AddRange(await dbContext.Farmers.ToListAsync(ct));
                people.AddRange(await dbContext.BlackSmiths.ToListAsync(ct));
                people.AddRange(await dbContext.Apothecaries.ToListAsync(ct));
                people.AddRange(await dbContext.Traders.ToListAsync(ct));

                foreach (var person in people)
                {
                    person.IsHungry = true;
                }
            }
            
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

            if (dbContext.Crops.SingleOrDefault(x => x.Id == 1)!.CropsCount >= _counter.PopulationCount
                && dbContext.Herbs.SingleOrDefault(x => x.Id == 1)!.HerbsCount >= 1 && dbContext.Wood.SingleOrDefault(x => x.Id == 1)!.WoodCount >= 1
                && dbContext.Medicines.SingleOrDefault(x => x.Id == 1)!.MedicineCount >= 1)
            {
                foreach (var item in hungryPeople)
                {
                    item.IsHungry = false;
                }
            }

            await dbContext.SaveChangesAsync(ct);
        }
    }

    /// <summary>
    /// Checks actual threat status
    /// </summary>
    /// <param name="currentThreat">Current threat in simulation</param>
    /// <param name="highestFarmingLevel"></param>
    /// <param name="medicineCount"></param>
    /// <param name="weaponryCount"></param>
    /// <param name="cropsCount"></param>
    /// <param name="ct">Cancellation token</param>
    /// <param name="highestMedicLevel"></param>
    /// <param name="highestSmithLevel"></param>
    public Task CheckThreatStatus(Threat currentThreat, int highestMedicLevel, int highestSmithLevel, int highestFarmingLevel, int medicineCount, int weaponryCount, int cropsCount, CancellationToken ct)
    {
        if (currentThreat.GetType() == typeof(FightingThreat))
        {
            var fThreat = (FightingThreat)currentThreat;

            if (fThreat.RequiredSmithingLevel > highestSmithLevel && fThreat.RequiredWeaponryCount > weaponryCount)
            {
                currentThreat.IsActive = true;
            }
            else
            {
                currentThreat.IsActive = false;
            }
        }
        if (currentThreat.GetType() == typeof(PlagueThreat))
        {
            var plagueThreat = (PlagueThreat)currentThreat;

            if (plagueThreat.RequiredMedicalLevel > highestMedicLevel && plagueThreat.RequiredMedicineCount > medicineCount)
            {
                currentThreat.IsActive = true;
            }
            else
            {
                currentThreat.IsActive = false;
            }
        }
        if (currentThreat.GetType() == typeof(NaturalThreat))
        {
            var nThreat = (NaturalThreat)currentThreat;

            if (nThreat.RequiredFarmingLevel > highestFarmingLevel && nThreat.RequiredCropsCount > cropsCount)
            {
                currentThreat.IsActive = true;
            }
            else
            {
                currentThreat.IsActive = false;
            }
        }
        
        return Task.CompletedTask;
    }
}