namespace ColonySimulator.persistence.Models.ModelsThreats;

public class NaturalThreat : Threat
{
    public int RequiredFarmingLevel { get; set; }
    public int RequiredCropsCount { get; set; }
}