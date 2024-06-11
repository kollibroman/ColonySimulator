using System.ComponentModel.DataAnnotations.Schema;

namespace ColonySimulator.Backend.Persistence.Models.Resources;

/// <summary>
/// Main class for resources
/// </summary>
public abstract class Resource
{
    /// <summary>
    /// Id of the resource
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    /// <summary>
    /// Name of resource
    /// </summary>
    public required string Name { get; set; }
}