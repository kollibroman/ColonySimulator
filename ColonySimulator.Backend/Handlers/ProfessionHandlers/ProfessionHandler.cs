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
       
        var affectedResources = _threatHandler.CalculateUsedResources(resources, _threatProvider.ThreatToExperience);

        foreach (var farmer in farmers)
        {
            await _farmerHandler.Farm(crop!, herbs!, farmer.FarmingLevel);

            await _threatHandler.CalculateAffection(farmer, _threatProvider.ThreatToExperience);
            var effect =
                await _threatHandler.GenerateEffects(_threatProvider.ThreatToExperience, affectedResources);

            await _farmerHandler.ExperienceThreat(effect, farmer, resources);
            await _dbContext.SaveChangesAsync();
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

        if (apothecaries.Count != 0 && herbs.HerbsCount != 0)
        {
            foreach (var apothecary in apothecaries)
            {
                await _apothecaryHandler.CollectingHerbs(herbs!, apothecary.ApothecaryLevel);
                await _apothecaryHandler.CreateMedicine(herbs!, medicine!, apothecary.ApothecaryLevel);
            
                await _threatHandler.CalculateAffection(apothecary, _threatProvider.ThreatToExperience);
                var effect = await _threatHandler.GenerateEffects(_threatProvider.ThreatToExperience, affectedResources);

                await _apothecaryHandler.ExperienceThreat(effect, apothecary, resources);
            }

            await _dbContext.SaveChangesAsync();
        }
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

        if (timbers.Count != 0)
        {
            foreach (var timber in timbers)
            {
                await _timberHandler.CreateWood(wood!, timber.TimberLevel);
            
                await _threatHandler.CalculateAffection(timber, _threatProvider.ThreatToExperience);
                var effect = await _threatHandler.GenerateEffects(_threatProvider.ThreatToExperience, affectedResources);

                await _timberHandler.ExperienceThreat(effect, timber, resources);
            }

            await _dbContext.SaveChangesAsync();
        }
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

        if (blackSmiths.Count != 0 && wood.WoodCount != 0)
        {
            foreach (var blacksmith in blackSmiths)
            {
                await _blackSmithHandler.CreateWeapon(weapon!, wood!, blacksmith.BlackSmithLevel);
            
                await _threatHandler.CalculateAffection(blacksmith, _threatProvider.ThreatToExperience);
                var effect = await _threatHandler.GenerateEffects(_threatProvider.ThreatToExperience, affectedResources);

                await _blackSmithHandler.ExperienceThreat(effect, blacksmith, resources);
            }

            await _dbContext.SaveChangesAsync();
        }
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

        if (medics.Count != 0 && medicine.MedicineCount != 0)
        {
            foreach (var medic in medics)
            {
                foreach (var sickPerson in sickPeople)
                {
                    await _medicHandler.Heal(medicine!, sickPerson, medic.MedicLevel);
                }
            
                await _threatHandler.CalculateAffection(medic, _threatProvider.ThreatToExperience);
                var effect = await _threatHandler.GenerateEffects(_threatProvider.ThreatToExperience, affectedResources);

                await _medicHandler.ExperienceThreat(effect, medic, resources);
            }

            await _dbContext.SaveChangesAsync();
        }
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
        var trader = await _dbContext.Traders.SingleOrDefaultAsync(x => x.Id == 1);

        var resources = new List<Resource>
        {
            crops!, medicine!, weaponry!, wood!, herbs!
        };
        
        var affectedResources = _threatHandler.CalculateUsedResources(resources, _threatProvider.ThreatToExperience);
        
        var effect = await _threatHandler.GenerateEffects(_threatProvider.ThreatToExperience, affectedResources);
        
        if(crops.CropsCount != 0 && wood.WoodCount != 0 && medicine.MedicineCount != 0 && herbs.HerbsCount != 0 && weaponry.WeaponryCount != 0)
        {
            await _traderHandler.Trade(crops!, wood!, medicine!, herbs!, weaponry!);
        
            await _threatHandler.CalculateAffection(trader, _threatProvider.ThreatToExperience);
            await _traderHandler.ExperienceThreat(effect, trader, resources);
            
            await _dbContext.SaveChangesAsync();
        }
    }
}