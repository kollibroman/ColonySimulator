using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Handlers.Interfaces;
using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;
using ColonySimulator.Backend.Persistence.Models.Threats;
using Microsoft.EntityFrameworkCore;

namespace ColonySimulator.Backend.Handlers;

/// <summary>
/// Implementation of IThreatHandler
/// </summary>
public class ThreatHandler : IThreatHandler
{
    /// <summary>
    /// Damage for an entity
    /// </summary>
    private int _damage;

    private readonly ColonySimulatorContext _dbContext;
    
    /// <summary>
    /// Constructor for class with DI parameters
    /// </summary>
    /// <param name="dbContext">Db context</param>
    public ThreatHandler(ColonySimulatorContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    /// <summary>
    /// Calculates affection by threat of entity
    /// </summary>
    /// <param name="profession">Profession of entity</param>
    /// <param name="threat">threat passed on as parameter</param>
    /// <returns></returns>
    public Task CalculateAffection(Proffesion profession, Threat threat)
    {
        _damage = profession switch
        {
            Medic medic => CalculateForMedic(medic, threat),
            Apothecary apothecary => CalculateForApothecary(apothecary, threat),
            BlackSmith smith => CalculateForBlackSmith(smith, threat),
            Farmer farmer => CalculateForFarmer(farmer, threat),
            Timber timber => CalculateForTimber(timber, threat),
            _ => _damage
        };

        return Task.CompletedTask;
    }

    /// <summary>
    /// Generates effect based on threat for entity
    /// </summary>
    /// <param name="threat">Current threat</param>
    /// <param name="resources">List of resources to pass</param>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>Generated effect</returns>
    public Task<Effect> GenerateEffects(Threat threat, List<Resource> resources)
    {
        ArgumentNullException.ThrowIfNull(threat);
        ArgumentNullException.ThrowIfNull(resources);

        Effect effect = threat switch
        {
            PlagueThreat plagueThreat => new PlagueEffect(
                name: plagueThreat.Name,
                damage: _damage,
                medicineCount: plagueThreat.RequiredMedicineCount,
                isSick: true,
                requiredMedicLevel: plagueThreat.RequiredMedicalLevel
            ),
        
            NaturalThreat naturalThreat => new NaturalEffect(
                name: naturalThreat.Name,
                damage: _damage,
                isHungry: true,
                resourcesLost: resources
            ),
        
            FightingThreat fightingThreat => new FightingThreatEffect(
                name: fightingThreat.Name,
                damage: _damage,
                resourcesStolen: resources
            ),
        
            _ => throw new InvalidOperationException("Unknown threat type")
        };

        return Task.FromResult(effect);
    }
    
    /// <summary>
    /// Generates random threat
    /// </summary>
    /// <param name="ct"></param>
    /// <returns>generated threat</returns>
    public async Task<Threat?> GenerateRandomThreat(CancellationToken ct)
    {
        var rnd = new Random();

        var rndThreatType = rnd.Next(1, 3);

        return rndThreatType switch
        {
            1 => await _dbContext.FightingThreats.SingleOrDefaultAsync(x => x.Id == rnd.Next(1,10), ct),
            2 => await _dbContext.NaturalThreats.SingleOrDefaultAsync(x => x.Id == rnd.Next(1,15), ct),
            3 => await _dbContext.PlagueThreats.SingleOrDefaultAsync(x => x.Id == rnd.Next(1,10), ct),
            _ => await _dbContext.PlagueThreats.SingleOrDefaultAsync(x => x.Id == 1, ct)
        };
    }

    private static int CalculateForMedic(Medic medic, Threat threat) => 
        threat switch
        {
            PlagueThreat => 0,
            NaturalThreat => threat.ThreatLevel * 10 - medic.MedicLevel * medic.Agility,
            FightingThreat => threat.ThreatLevel * 10 - medic.MedicLevel * medic.Strength,
            _ => 1
        };

    private static int CalculateForApothecary(Apothecary apothecary, Threat threat) => 
        threat switch
        {
            PlagueThreat => 0,
            NaturalThreat => threat.ThreatLevel * 10 - apothecary.ApothecaryLevel * apothecary.Agility,
            FightingThreat => threat.ThreatLevel * 10 - apothecary.ApothecaryLevel * apothecary.Strength,
            _ => 1
        };

    private static int CalculateForBlackSmith(BlackSmith blackSmith, Threat threat) => 
        threat switch
        {
            FightingThreat => 0,
            PlagueThreat => threat.ThreatLevel * 10 - blackSmith.BlackSmithLevel * blackSmith.Vitality,
            NaturalThreat => threat.ThreatLevel * 10 - blackSmith.BlackSmithLevel * blackSmith.Agility,
            _ => 1
        };

    private static int CalculateForFarmer(Farmer farmer, Threat threat) =>
        threat switch
        {
            PlagueThreat => threat.ThreatLevel * 10 - farmer.Vitality * farmer.FarmingLevel,
            NaturalThreat => threat.ThreatLevel * 10 - farmer.FarmingLevel * farmer.Agility,
            FightingThreat => threat.ThreatLevel * 10 - farmer.FarmingLevel * farmer.Strength,
            _ => 1
        };

    private static int CalculateForTimber(Timber timber, Threat threat) =>
        threat switch
        {
            PlagueThreat => threat.ThreatLevel * 9 - timber.TimberLevel * timber.Vitality,
            NaturalThreat => threat.ThreatLevel * 9 - timber.TimberLevel * timber.Agility,
            FightingThreat => threat.ThreatLevel * 9 - timber.TimberLevel * timber.Strength,
            _ => 1
        };
}