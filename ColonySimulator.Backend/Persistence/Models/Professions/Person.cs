using System.ComponentModel.DataAnnotations.Schema;
using ColonySimulator.Backend.Persistence.Enums;

namespace ColonySimulator.Backend.Persistence.Models.Professions;

/// <summary>
/// Main person entity model
/// </summary>
public abstract class Person
{
    /// <summary>
    /// Id of entity
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    /// <summary>
    /// Vitality of entity
    /// </summary>
    public int Vitality { get; set; }
    
    /// <summary>
    /// Strength of entity
    /// </summary>
    public int Strength { get; set; }
    
    /// <summary>
    /// Agility of entity
    /// </summary>
    public int Agility { get; set; }
    
    /// <summary>
    /// Gender of entity
    /// </summary>
    public Gender Gender { get; set; }
    
    /// <summary>
    /// Resource consumption of entity
    /// </summary>
    public int ResourceConsumption { get; set; }
    
    /// <summary>
    /// Sick status of entity
    /// </summary>
    public bool IsSick { get; set; }
    
    /// <summary>
    /// Hunger status of entity
    /// </summary>
    public bool IsHungry { get; set; }
}