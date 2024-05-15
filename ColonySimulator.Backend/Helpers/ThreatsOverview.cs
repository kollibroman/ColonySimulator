using ColonySimulator.Backend.Persistence.Models.Threats;

namespace ColonySimulator.Backend.Helpers;

public class ThreatsOverview
{
    public ICollection<Threat> ThreatsDefeated { get; set; } = default!;
    public ICollection<Threat> ThreatsYieldedTo { get; set; } = default!;
}