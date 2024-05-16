namespace ColonySimulator.Backend.Persistence.Models.Threats;

public class NaturalThreat : Threat
{
    public int RequiredFarmingLevel { get; set; }
    public int RequiredCropsCount { get; set; }
}