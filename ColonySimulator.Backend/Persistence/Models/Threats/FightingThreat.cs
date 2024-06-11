namespace ColonySimulator.Backend.Persistence.Models.Threats;

/// <summary>
/// FightingThreat entity model
/// </summary>
public class FightingThreat : Threat
{   
    /// <summary>
    /// Required level of blacksmith
    /// </summary>
    public int RequiredSmithingLevel { get; set; }
    
    /// <summary>
    /// Required weaponry count
    /// </summary>
    public int RequiredWeaponryCount { get; set; }
}