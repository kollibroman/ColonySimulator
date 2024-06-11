namespace ColonySimulator.Backend.Persistence.Models.Professions;

/// <summary>
/// Trader entity model
/// </summary>
public class Trader : Proffesion
{
    /// <summary>
    /// Level of entity
    /// </summary>
    public int TradingLevel { get; set; }
}