using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Handlers.Interfaces;
using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;
using ColonySimulator.Backend.Persistence.Models.Threats;
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
    private readonly IThreatHandler _threatHandler;
    private readonly ColonySimulatorContext _dbContext;
    
    /// <summary>
    /// Global threat to experience for an entity
    /// </summary>
    public Threat ThreatToExperience { get; set; } = default!;

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
    public ProfessionHandler(IFarmerHandler farmerHandler, IApothecaryHandler apothecaryHandler, IBlackSmithHandler blackSmithHandler,
                                IMedicHandler medicHandler, ITimberHandler timberHandler, ITraderHandler traderHandler, ColonySimulatorContext dbContext, IThreatHandler threatHandler)
    {
        _farmerHandler = farmerHandler;
        _apothecaryHandler = apothecaryHandler;
        _blackSmithHandler = blackSmithHandler;
        _medicHandler = medicHandler;
        _timberHandler = timberHandler;
        _traderHandler = traderHandler;
        _dbContext = dbContext;
        _threatHandler = threatHandler;
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
        
        for(int i = 1; i < farmers.Count; i++)
        {
            await _farmerHandler.Farm(crop!, herbs!, farmers[i].FarmingLevel);

            await _threatHandler.CalculateAffection(farmers[i], ThreatToExperience);
            var effect = await _threatHandler.GenerateEffects(ThreatToExperience, resources);

            await _farmerHandler.ExperienceThreat(effect);
        }

        await _dbContext.SaveChangesAsync();
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

        for(int i = 1; i < apothecaries.Count; i++)
        {
            await _apothecaryHandler.CollectingHerbs(herbs!, apothecaries[i].ApothecaryLevel);
            await _apothecaryHandler.CreateMedicine(herbs!, medicine!, apothecaries[i].ApothecaryLevel);
            
            await _threatHandler.CalculateAffection(apothecaries[i], ThreatToExperience);
            var effect = await _threatHandler.GenerateEffects(ThreatToExperience, resources);

            await _apothecaryHandler.ExperienceThreat(effect);
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
       
        for(int i = 1; i < timbers.Count; i++)
        {
            await _timberHandler.CreateWood(wood!, timbers[i].TimberLevel);
            
            await _threatHandler.CalculateAffection(timbers[i], ThreatToExperience);
            var effect = await _threatHandler.GenerateEffects(ThreatToExperience, resources);

            await _timberHandler.ExperienceThreat(effect);
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

        for (int i = 1; i < blackSmiths.Count; i++)
        {
            await _blackSmithHandler.CreateWeapon(weapon!, wood!, blackSmiths[i].BlackSmithLevel);
            
            await _threatHandler.CalculateAffection(blackSmiths[i], ThreatToExperience);
            var effect = await _threatHandler.GenerateEffects(ThreatToExperience, resources);

            await _blackSmithHandler.ExperienceThreat(effect);
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

        for (int i = 1; i < medics.Count; i++)
        {
            foreach (var sickPerson in sickPeople)
            {
                await _medicHandler.Heal(medicine!, sickPerson, medics[i].MedicLevel);
            }
            
            await _threatHandler.CalculateAffection(medics[i], ThreatToExperience);
            var effect = await _threatHandler.GenerateEffects(ThreatToExperience, resources);

            await _medicHandler.ExperienceThreat(effect);
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
        
        await _traderHandler.Trade(crops!, wood!, medicine!, herbs!, weaponry!);
        
        await _dbContext.SaveChangesAsync();
    }
}