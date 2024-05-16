namespace ColonySimulator.Backend.Persistence.Models.Threats;

public abstract class Threat
{
    public int Id { get; set; }
    public int ThreatLevel { get; set; }
    public required string Name { get; set; }
}