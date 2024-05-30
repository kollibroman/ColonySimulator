using System.Diagnostics.CodeAnalysis;
using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Effects;

/// <summary>
/// Threat effect from natural effect
/// </summary>
public class NaturalEffect : Effect
{
    public NaturalEffect()
    {
    }

    [SetsRequiredMembers]
    public NaturalEffect(string name, int damage, bool isHungry, List<Resource> resourcesLost)
    {
        Name = name;
        Damage = damage;
        IsHungry = isHungry;
        ResourcesLost = resourcesLost;
    }

    /// <summary>
    /// Bool setting entity hunger status to true
    /// </summary>
    public required bool IsHungry { get; init; } = true;
    
    /// <summary>
    /// Resources lost due to threat
    /// </summary>
    public required List<Resource> ResourcesLost { get; init; }
}