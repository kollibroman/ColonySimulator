using System.Diagnostics.CodeAnalysis;
using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Effects;

/// <summary>
/// Threat effect from combat type threat
/// </summary>
public class FightingThreatEffect : Effect
{
    /// <summary>
    /// Empty contstructor
    /// </summary>
    public FightingThreatEffect()
    {
    }
    
    /// <summary>
    /// Constructor with parameters
    /// </summary>
    /// <param name="name">Name of effect</param>
    /// <param name="damage">damage done to entities</param>
    /// <param name="resourcesStolen">resources lost</param>
    [SetsRequiredMembers]
    public FightingThreatEffect(string name, int damage, List<Resource> resourcesStolen)
    {
        Name = name;
        Damage = damage;
        ResourcesStolen = resourcesStolen;
    }

    /// <summary>
    /// Resources stolen from colony
    /// </summary>
    public required List<Resource> ResourcesStolen { get; init; }
}