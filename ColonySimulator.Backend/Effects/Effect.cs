namespace ColonySimulator.Backend.Effects;

/// <summary>
/// General effects done to entities
/// </summary>
public abstract class Effect
{
    /// <summary>
    /// Name of effect
    /// </summary>
    public required string Name { get; init; }
    /// <summary>
    /// Damage done to single entity
    /// </summary>
    public required int Damage { get; init; }
}