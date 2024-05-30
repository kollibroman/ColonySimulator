using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Effects;

/// <summary>
/// Threat effect from combat type threat
/// </summary>
public class FightingThreatEffect : Effect
{
    
    /// <summary>
    /// Resources stolen from colony
    /// </summary>
    public required List<Resource> ResourcesStolen { get; init; }
}