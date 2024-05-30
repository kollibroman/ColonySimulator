using System.ComponentModel.DataAnnotations.Schema;
using ColonySimulator.Backend.Persistence.Enums;

namespace ColonySimulator.Backend.Persistence.Models.Professions;

public abstract class Person
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int Vitality { get; set; }
    public int Strength { get; set; }
    public int Agility { get; set; }
    public Gender Gender { get; set; }
    public int ResourceConsumption { get; set; }
    public bool IsSick { get; set; }
    public bool IsHungry { get; set; }
}