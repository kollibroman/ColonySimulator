using ColonySimulator.Backend.Persistence.Models.Threats;

namespace ColonySimulator.Backend.Helpers;

public class ThreatsOverview
{
    public ICollection<PlagueThreat> ThreatsDefeated { get; set; } = default!;
    public ICollection<PlagueThreat> ThreatsYieldedTo { get; set; } = default!;
}