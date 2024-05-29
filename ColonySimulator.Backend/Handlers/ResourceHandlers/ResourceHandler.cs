using ColonySimulator.Backend.Handlers.Interfaces;
using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;
using Microsoft.EntityFrameworkCore;

namespace ColonySimulator.Backend.Handlers.ResourceHandlers;

public class ResourceHandler : IResourceHandler
{
    private ColonySimulatorContext _dbContext;
    public ResourceHandler(ColonySimulatorContext dbContext)
    {
        _dbContext = dbContext;
    }
    
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

        for (int i = 0; i < popCount; i++)
        {
            if(crop.CropsCount - 1 < 0){}
            else
            {
                crop.CropsCount--;
            }
        }
    }

    
}