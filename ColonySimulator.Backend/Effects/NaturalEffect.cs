using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Effects;

/// <summary>
/// Threat effect from natural effect
/// </summary>
public class NaturalEffect : Effect
{
    /// <summary>
    /// Bool setting entity hunger status to true
    /// </summary>
    public required bool IsHungry { get; init; } = true;
    
    /// <summary>
    /// Resources lost due to threat
    /// </summary>
    public required List<Resource> ResourcesLost { get; init; }
}