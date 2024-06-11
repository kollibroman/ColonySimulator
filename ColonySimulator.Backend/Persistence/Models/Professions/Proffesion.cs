namespace ColonySimulator.Backend.Persistence.Models.Professions;

/// <summary>
/// Profession entity model
/// </summary>
public class Proffesion : Person
{
    /// <summary>
    /// Required strength for profession
    /// </summary>
    public double RequiredStrength { get; set; }
    
    /// <summary>
    /// Required agility for profession
    /// </summary>
    public double RequiredAgility { get; set; }
}