using System.Diagnostics.CodeAnalysis;
using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Effects;

/// <summary>
/// Threat effect from natural effect
/// </summary>
public class NaturalEffect : Effect
{
    /// <summary>
    /// empty constructor
    /// </summary>
    public NaturalEffect()
    {
    }
    
    /// <summary>
    /// Constructor with parameters to pass
    /// </summary>
    /// <param name="name">Name of effect</param>
    /// <param name="damage">damage done to entity</param>
    /// <param name="isHungry">parameter to set in entity</param>
    /// <param name="resourcesLost">resources lost due to threat</param>
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