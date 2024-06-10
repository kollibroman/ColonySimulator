using ColonySimulator.Backend.Persistence.Models.Threats;

namespace ColonySimulator.Backend.Helpers.Interfaces;

/// <summary>
/// Provides threat to experience by entity
/// </summary>
public interface IThreatProvider
{
    /// <summary>
    /// Threat to experience by entity
    /// </summary>
    public Threat? ThreatToExperience { get; set; }
}