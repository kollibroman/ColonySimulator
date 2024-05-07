namespace ColonySimulator.persistence.Models;

public abstract class Threat
{
    public int Id { get; set; }
    public int ThreatLevel { get; set; }
    public String Name { get; set; }
}