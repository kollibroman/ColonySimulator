namespace ColonySimulator.persistence.Models;

public abstract class Person
{
    public int Id { get; set; }
    public double Vitality { get; set; }
    public double ResourceConsumption { get; set; }
}