namespace ColonySimulator.persistence.Models.ModelsThreats;

public class Threat
{
    public int Id { get; set; }
    public int ThreatLevel { get; set; }
    public required string Name { get; set; }
}