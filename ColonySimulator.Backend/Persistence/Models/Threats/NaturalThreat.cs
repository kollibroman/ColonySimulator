namespace ColonySimulator.Backend.Persistence.Models.Threats;

/// <summary>
/// NaturalThreat entity model
/// </summary>
public class NaturalThreat : Threat
{
    /// <summary>
    /// Required level of farming
    /// </summary>
    public int RequiredFarmingLevel { get; set; }
    
    /// <summary>
    /// Required crops count
    /// </summary>
    public int RequiredCropsCount { get; set; }
}