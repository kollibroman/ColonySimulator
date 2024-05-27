using System.ComponentModel.DataAnnotations.Schema;

namespace ColonySimulator.Backend.Persistence.Models.Threats;

public abstract class Threat
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int ThreatLevel { get; set; }
    public required string Name { get; set; }
}