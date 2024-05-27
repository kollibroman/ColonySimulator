using ColonySimulator.Backend.Persistence.Models.Threats;

namespace ColonySimulator.Backend.Helpers;

/// <summary>
/// Overview class for threats
/// </summary>
public class ThreatsOverview //ADD OTHER OBJECT, THIS IS FOR PURE SHOWCASE AT TUESDAY
{
    /// <summary>
    /// Defeated threats list
    /// </summary>
    public ICollection<PlagueThreat> ThreatsDefeated { get; set; } = default!;
    
    /// <summary>
    /// Not defeated threat list 
    /// </summary>
    public ICollection<PlagueThreat> ThreatsYieldedTo { get; set; } = default!;
}