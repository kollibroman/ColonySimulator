namespace ColonySimulator.Backend.Persistence.Models.Resources;

public abstract class Resource
{
    public int Id { get; set; }
    public required string Name { get; set; }
}