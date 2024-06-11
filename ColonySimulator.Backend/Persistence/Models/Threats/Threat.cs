using System.ComponentModel.DataAnnotations.Schema;

namespace ColonySimulator.Backend.Persistence.Models.Threats;

/// <summary>
/// Main class for threat
/// </summary>
public abstract class Threat
{
    /// <summary>
    /// Id of the threat
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    /// <summary>
    /// Level of threat
    /// </summary>
    public int ThreatLevel { get; set; }
    
    /// <summary>
    /// Name of threat
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Activity status check
    /// </summary>
    public bool IsActive { get; set; }
}