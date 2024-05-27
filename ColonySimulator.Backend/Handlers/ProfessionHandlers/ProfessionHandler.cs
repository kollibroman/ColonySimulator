using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;
using Microsoft.EntityFrameworkCore;

namespace ColonySimulator.Backend.Handlers.ProfessionHandlers;

/// <summary>
/// Profession handler, it resolves and modifies data related to simuation
/// </summary>

public class ProfessionHandler : IProfessionHandler
{
    
    private readonly IFarmerHandler _farmerHandler;
    private readonly IApothecaryHandler _apothecaryHandler;
    private readonly IBlackSmithHandler _blackSmithHandler;
    private readonly IMedicHandler _medicHandler;
    private readonly ITimberHandler _timberHandler;
    private readonly ITraderHandler _traderHandler;
    private ColonySimulatorContext _dbContext;

    /// <summary>
    /// Constructor with DI parameters 
    /// </summary>
    /// <param name="farmerHandler">farmer handler</param>
    /// <param name="apothecaryHandler">apothecary handler</param>
    /// <param name="blackSmithHandler">blacksmith handler</param>
    /// <param name="medicHandler">medic handler</param>
    /// <param name="timberHandler">timber handler</param>
    /// <param name="traderHandler">trader handler</param>
    /// <param name="dbContext">Db context class with db objects</param>
    
    public ProfessionHandler(IFarmerHandler farmerHandler, IApothecaryHandler apothecaryHandler, IBlackSmithHandler blackSmithHandler,
                                IMedicHandler medicHandler, ITimberHandler timberHandler, ITraderHandler traderHandler, ColonySimulatorContext dbContext)
    {
        _farmerHandler = farmerHandler;
        _apothecaryHandler = apothecaryHandler;
        _blackSmithHandler = blackSmithHandler;
        _medicHandler = medicHandler;
        _timberHandler = timberHandler;
        _traderHandler = traderHandler;
        _dbContext = dbContext;
    }

    public async Task HandleFarm()
    {
        var farmers = await _dbContext.Farmers.ToListAsync();
        var crop = await _dbContext.Crops.SingleOrDefaultAsync(x => x.Id == 1);
        
        foreach (var farmer in farmers)
        {
            await _farmerHandler.Farm(crop);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task HandleApothecary()
    {
        throw new NotImplementedException();
    }
    
    public async Task HandleBlackSmith()
    {
        throw new NotImplementedException();
    }
    
    public async Task HandleMedic()
    {
        throw new NotImplementedException();
    }
    
    public async Task HandleTimber()
    {
        throw new NotImplementedException();
    }
    
    public async Task HandleTrader()
    {
        throw new NotImplementedException();
    }
}