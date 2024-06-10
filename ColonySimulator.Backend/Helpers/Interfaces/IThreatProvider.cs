using ColonySimulator.Backend.Persistence.Models.Threats;

namespace ColonySimulator.Backend.Helpers.Interfaces;

public interface IThreatProvider
{
    public Threat? ThreatToExperience { get; set; }
}