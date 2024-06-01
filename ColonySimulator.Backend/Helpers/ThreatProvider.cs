using ColonySimulator.Backend.Persistence.Models.Threats;

namespace ColonySimulator.Backend.Helpers;

/// <summary>
/// Global threat storing class
/// </summary>
public class ThreatProvider
{
    /// <summary>
    /// Threat to experience for a class
    /// </summary>
    public Threat? ThreatToExperience { get; set; }
}