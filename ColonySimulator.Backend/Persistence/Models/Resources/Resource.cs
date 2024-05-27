using System.ComponentModel.DataAnnotations.Schema;

namespace ColonySimulator.Backend.Persistence.Models.Resources;

public abstract class Resource
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Name { get; set; }
}