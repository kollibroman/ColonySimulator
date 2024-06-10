using ColonySimulator.Backend.Handlers.Interfaces;
using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Helpers;
using ColonySimulator.Backend.Helpers.Interfaces;
using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;
using Microsoft.EntityFrameworkCore;

namespace ColonySimulator.Backend.Handlers.ProfessionHandlers;

/// <summary>
/// Profession handler, it resolves and modifies data related to simulation
/// </summary>
public class ProfessionHandler : IProfessionHandler
{
    
    private readonly IFarmerHandler _farmerHandler;
    private readonly IApothecaryHandler _apothecaryHandler;
    private readonly IBlackSmithHandler _blackSmithHandler;
    private readonly IMedicHandler _medicHandler;
    private readonly ITimberHandler _timberHandler;
    private readonly ITraderHandler _traderHandler;
    private readonly IThreatHandler _threatHandler;
    private readonly ColonySimulatorContext _dbContext;
    private readonly IThreatProvider _threatProvider;

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
    /// <param name="threatHandler">Threat handler interface</param>
    /// <param name="threatProvider">Threat provider class</param>
    public ProfessionHandler(IFarmerHandler farmerHandler, IApothecaryHandler apothecaryHandler, IBlackSmithHandler blackSmithHandler,
                                IMedicHandler medicHandler, ITimberHandler timberHandler, ITraderHandler traderHandler, ColonySimulatorContext dbContext, IThreatHandler threatHandler, IThreatProvider threatProvider)
    {
        _farmerHandler = farmerHandler;
        _apothecaryHandler = apothecaryHandler;
        _blackSmithHandler = blackSmithHandler;
        _medicHandler = medicHandler;
        _timberHandler = timberHandler;
        _traderHandler = traderHandler;
        _dbContext = dbContext;
        _threatHandler = threatHandler;
        _threatProvider = threatProvider;
    }
    
    //All Handlers have to be balanced for later good working of program

    /// <summary>
    /// Handles farmers
    /// </summary>
    public async Task HandleFarm()
    {
        var farmers = await _dbContext.Farmers.ToListAsync();
        var crop = await _dbContext.Crops.SingleOrDefaultAsync(x => x.Id == 1);
        var herbs = await _dbContext.Herbs.SingleOrDefaultAsync(x => x.Id == 1);

        var resources = new List<Resource>
        {
            crop!, herbs!
        };

        if (_threatProvider.ThreatToExperience is not null)
        {
            var affectedResources = _threatHandler.CalculateUsedResources(resources, _threatProvider.ThreatToExperience);
        
            for(int i = 0; i < farmers.Count; i++)
            {
                await _farmerHandler.Farm(crop!, herbs!, farmers[i].FarmingLevel);

                await _threatHandler.CalculateAffection(farmers[i], _threatProvider.ThreatToExperience);
                var effect = await _threatHandler.GenerateEffects(_threatProvider.ThreatToExperience, affectedResources);

                await _farmerHandler.ExperienceThreat(effect, farmers[i], resources);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
    
    /// <summary>
    /// Handles apothecaries
    /// </summary>
    public async Task HandleApothecary()
    {
        var apothecaries = await _dbContext.Apothecaries.ToListAsync();
        var herbs = await _dbContext.Herbs.SingleOrDefaultAsync(x => x.Id == 1);
        var medicine = await _dbContext.Medicines.SingleOrDefaultAsync(x => x.Id == 1);

        var resources = new List<Resource>
        {
            herbs!, medicine!
        };
        
        var affectedResources = _threatHandler.CalculateUsedResources(resources, _threatProvider.ThreatToExperience);

        for(int i = 0; i < apothecaries.Count; i++)
        {
            await _apothecaryHandler.CollectingHerbs(herbs!, apothecaries[i].ApothecaryLevel);
            await _apothecaryHandler.CreateMedicine(herbs!, medicine!, apothecaries[i].ApothecaryLevel);
            
            await _threatHandler.CalculateAffection(apothecaries[i], _threatProvider.ThreatToExperience);
            var effect = await _threatHandler.GenerateEffects(_threatProvider.ThreatToExperience, affectedResources);

            await _apothecaryHandler.ExperienceThreat(effect, apothecaries[i], resources);
        }
        
        await _dbContext.SaveChangesAsync();
    }
    
    /// <summary>
    /// Handles timbers
    /// </summary>
    public async Task HandleTimber()
    {
        var timbers = await _dbContext.Timbers.ToListAsync();
        var wood = await _dbContext.Wood.SingleOrDefaultAsync(x => x.Id == 1);

        var resources = new List<Resource>
        {
            wood!
        };

        var affectedResources = _threatHandler.CalculateUsedResources(resources, _threatProvider.ThreatToExperience);
        
        for(int i = 0; i < timbers.Count; i++)
        {
            await _timberHandler.CreateWood(wood!, timbers[i].TimberLevel);
            
            await _threatHandler.CalculateAffection(timbers[i], _threatProvider.ThreatToExperience);
            var effect = await _threatHandler.GenerateEffects(_threatProvider.ThreatToExperience, affectedResources);

            await _timberHandler.ExperienceThreat(effect, timbers[i], resources);
        }
        
        await _dbContext.SaveChangesAsync();
    }
    
    /// <summary>
    /// handles blacksmiths
    /// </summary>
    public async Task HandleBlackSmith()
    {
        var blackSmiths = await _dbContext.BlackSmiths.ToListAsync();
        var wood = await _dbContext.Wood.SingleOrDefaultAsync(x => x.Id == 1);
        var weapon = await _dbContext.Weaponry.SingleOrDefaultAsync(x => x.Id == 1);

        var resources = new List<Resource>
        {
            wood!, weapon!
        };
        
        var affectedResources = _threatHandler.CalculateUsedResources(resources, _threatProvider.ThreatToExperience);

        for (int i = 0; i < blackSmiths.Count; i++)
        {
            await _blackSmithHandler.CreateWeapon(weapon!, wood!, blackSmiths[i].BlackSmithLevel);
            
            await _threatHandler.CalculateAffection(blackSmiths[i], _threatProvider.ThreatToExperience);
            var effect = await _threatHandler.GenerateEffects(_threatProvider.ThreatToExperience, affectedResources);

            await _blackSmithHandler.ExperienceThreat(effect, blackSmiths[i], resources);
        }
        
        await _dbContext.SaveChangesAsync();
    }
    
    /// <summary>
    /// Handles medics
    /// </summary>
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

        var resources = new List<Resource>
        {
            medicine!
        };
        
        var affectedResources = _threatHandler.CalculateUsedResources(resources, _threatProvider.ThreatToExperience);

        for (int i = 0; i < medics.Count; i++)
        {
            foreach (var sickPerson in sickPeople)
            {
                await _medicHandler.Heal(medicine!, sickPerson, medics[i].MedicLevel);
            }
            
            await _threatHandler.CalculateAffection(medics[i], _threatProvider.ThreatToExperience);
            var effect = await _threatHandler.GenerateEffects(_threatProvider.ThreatToExperience, affectedResources);

            await _medicHandler.ExperienceThreat(effect, medics[i], resources);
        }
        
        await _dbContext.SaveChangesAsync();
    }
    
    /// <summary>
    /// Handles trading
    /// </summary>
    public async Task HandleTrader()
    {
        var crops = await _dbContext.Crops.SingleOrDefaultAsync(x => x.Id == 1);
        var wood = await _dbContext.Wood.SingleOrDefaultAsync(x => x.Id == 1);
        var medicine = await _dbContext.Medicines.SingleOrDefaultAsync(x => x.Id == 1);
        var herbs = await _dbContext.Herbs.SingleOrDefaultAsync(x => x.Id == 1);
        var weaponry = await _dbContext.Weaponry.SingleOrDefaultAsync(x => x.Id == 1);
        var trader = await _dbContext.Traders.ToListAsync();

        var resources = new List<Resource>
        {
            crops!, medicine!, weaponry!, wood!, herbs!
        };
        
        var affectedResources = _threatHandler.CalculateUsedResources(resources, _threatProvider.ThreatToExperience ?? throw new NullReferenceException());
        
        var effect = await _threatHandler.GenerateEffects(_threatProvider.ThreatToExperience, affectedResources);

        if (trader.Count != 0)
        {
            await _traderHandler.Trade(crops!, wood!, medicine!, herbs!, weaponry!);
            await _traderHandler.ExperienceThreat(effect, await _dbContext.Traders.SingleOrDefaultAsync(x => x.Id == 1) ?? throw new NullReferenceException("It is nulL!"),
                resources);
        }
        
        await _dbContext.SaveChangesAsync();
    }
}