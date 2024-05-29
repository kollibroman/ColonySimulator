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
    
    //All Handlers have to be balanced for later good working of program

    public async Task HandleFarm()
    {
        var farmers = await _dbContext.Farmers.ToListAsync();
        var crop = await _dbContext.Crops.SingleOrDefaultAsync(x => x.Id == 1);
        var herbs = await _dbContext.Herbs.SingleOrDefaultAsync(x => x.Id == 1);
        
        for(int i = 1; i < farmers.Count; i++)
        {
            await _farmerHandler.Farm(crop, herbs, farmers[i].FarmingLevel);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task HandleApothecary()
    {
        var apothecaries = await _dbContext.Apothecaries.ToListAsync();
        var herbs = await _dbContext.Herbs.SingleOrDefaultAsync(x => x.Id == 1);
        var medicine = await _dbContext.Medicines.SingleOrDefaultAsync(x => x.Id == 1);

        for(int i = 1; i < apothecaries.Count; i++)
        {
            await _apothecaryHandler.CollectingHerbs(herbs, apothecaries[i].ApothecaryLevel);
            await _apothecaryHandler.CreateMedicine(herbs, medicine, apothecaries[i].ApothecaryLevel);
        }
        
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task HandleTimber()
    {
        var timbers = await _dbContext.Timbers.ToListAsync();
        var wood = await _dbContext.Wood.SingleOrDefaultAsync(x => x.Id == 1);
       
        for(int i = 1; i < timbers.Count; i++)
        {
            _timberHandler.CreateWood(wood, timbers[i].TimberLevel);
        }
        
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task HandleBlackSmith()
    {
        var blackSmiths = await _dbContext.BlackSmiths.ToListAsync();
        var wood = await _dbContext.Wood.SingleOrDefaultAsync(x => x.Id == 1);
        var weapon = await _dbContext.Weaponry.SingleOrDefaultAsync(x => x.Id == 1);

        for (int i = 1; i < blackSmiths.Count; i++)
        {
            _blackSmithHandler.CreateWeapon(weapon, wood, blackSmiths[i].BlackSmithLevel);
        }
        
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task HandleMedic()
    {
        var medics = await _dbContext.Medics.ToListAsync();
        var medicine = await _dbContext.Medicines.SingleOrDefaultAsync(x => x.Id == 1);
        var sickPeople = new List<Person>();
        sickPeople.AddRange(await _dbContext.Medics.Where(x => x.IsSick == true).ToListAsync());
        sickPeople.AddRange(await _dbContext.Timbers.Where(x => x.IsSick == true).ToListAsync());
        sickPeople.AddRange(await _dbContext.Farmers.Where(x => x.IsSick == true).ToListAsync());
        sickPeople.AddRange(await _dbContext.BlackSmiths.Where(x => x.IsSick == true).ToListAsync());
        sickPeople.AddRange(await _dbContext.Apothecaries.Where(x => x.IsSick == true).ToListAsync());
        sickPeople.AddRange(await _dbContext.Traders.Where(x => x.IsSick == true).ToListAsync());

        for (int i = 1; i < medics.Count; i++)
        {
            foreach (var sickPerson in sickPeople)
            {
                _medicHandler.Heal(medicine, sickPerson, medics[i].MedicLevel);
            }
        }
        
        await _dbContext.SaveChangesAsync();
    }
    
    
    
    public async Task HandleTrader()
    {
        var crops = await _dbContext.Crops.SingleOrDefaultAsync(x => x.Id == 1);
        var wood = await _dbContext.Wood.SingleOrDefaultAsync(x => x.Id == 1);
        var medicine = await _dbContext.Medicines.SingleOrDefaultAsync(x => x.Id == 1);
        var herbs = await _dbContext.Herbs.SingleOrDefaultAsync(x => x.Id == 1);
        var weaponry = await _dbContext.Weaponry.SingleOrDefaultAsync(x => x.Id == 1);
        
        _traderHandler.Trade(crops, wood, medicine, herbs, weaponry);
        
        await _dbContext.SaveChangesAsync();
    }
}