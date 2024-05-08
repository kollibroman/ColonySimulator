namespace ColonySimulator.persistence.Models;

public abstract class Threat
{
    public int Id { get; set; }
    public int ThreatLevel { get; set; }
    public required string Name { get; init; }
}