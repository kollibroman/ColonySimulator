using System.Diagnostics.CodeAnalysis;
using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Effects;

/// <summary>
/// Threat effect from combat type threat
/// </summary>
public class FightingThreatEffect : Effect
{
    public FightingThreatEffect()
    {
    }

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