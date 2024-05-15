namespace ColonySimulator.persistence.Models.ModelsProfessions;

public abstract class Person
{
    public int Id { get; set; }
    public double Vitality { get; set; }
    public double Strength { get; set; }
    public double Agility { get; set; }
    public double ResourceConsumption { get; set; }
}