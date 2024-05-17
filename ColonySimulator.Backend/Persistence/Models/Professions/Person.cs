using ColonySimulator.Backend.Persistence.Enums;

namespace ColonySimulator.Backend.Persistence.Models.Professions;

public abstract class Person
{
    public int Id { get; set; }
    public double Vitality { get; set; }
    public double Strength { get; set; }
    public double Agility { get; set; }
    public Gender Gender { get; set; }
    public double ResourceConsumption { get; set; }
    public bool IsSick { get; set; }
}