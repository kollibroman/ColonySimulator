using ColonySimulator.Backend.Handlers.Interfaces;
using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;
using Microsoft.EntityFrameworkCore;

namespace ColonySimulator.Backend.Handlers.ResourceHandlers;

/// <summary>
/// Handler for resources
/// </summary>
public class ResourceHandler : IResourceHandler
{
    private ColonySimulatorContext _dbContext;
    
    /// <summary>
    /// Constructor for this handler
    /// </summary>
    /// <param name="dbContext">Database context</param>
    public ResourceHandler(ColonySimulatorContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    /// <summary>
    /// Handles resource consumption
    /// </summary>
    /// <param name="popCount">Population counter</param>
    public async Task ConsumeResources(int popCount)
    {
        var peopleList = new List<Person>();
        peopleList.AddRange(await _dbContext.Farmers.ToListAsync());
        peopleList.AddRange(await _dbContext.Apothecaries.ToListAsync());
        peopleList.AddRange(await _dbContext.Medics.ToListAsync());
        peopleList.AddRange(await _dbContext.Timbers.ToListAsync());
        peopleList.AddRange(await _dbContext.Traders.ToListAsync());
        peopleList.AddRange(await _dbContext.BlackSmiths.ToListAsync());

        var crop = await _dbContext.Crops.SingleOrDefaultAsync(x => x.Id == 1);
        var herbs = await _dbContext.Herbs.SingleOrDefaultAsync(x => x.Id == 1);
        var wood = await _dbContext.Wood.SingleOrDefaultAsync(x => x.Id == 1);

        foreach (var person in peopleList)
        {
            //Need to add dying people from starvation later
            if (crop.CropsCount - person.ResourceConsumption < 0)
            {
                crop.CropsCount = 0;    
            }
            else
            {
                crop.CropsCount -= person.ResourceConsumption/4;
            }
            
            if(herbs.HerbsCount - person.ResourceConsumption < 0)
            {
                herbs.HerbsCount = 0;
            }
            else
            {
                herbs.HerbsCount -= person.ResourceConsumption/4;
            }

            if (wood.WoodCount - person.ResourceConsumption < 0)
            {
                wood.WoodCount = 0;
            }
            else
            {
                wood.WoodCount -= person.ResourceConsumption/4;
            }
        }
    }
}