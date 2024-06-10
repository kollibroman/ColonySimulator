using ColonySimulator.Backend.Persistence.Models.Professions;

namespace ColonySimulator.Backend.Helpers;

/// <summary>
/// Overview class for professions
/// </summary>
public class ProfessionsOverview
{
    /// <summary>
    /// Apothecaries list
    /// </summary>
    public ICollection<Apothecary> Apothecaries { get; init; } = default!;
    
    /// <summary>
    /// BlackSmith list
    /// </summary>
    public ICollection<BlackSmith> BlackSmiths { get; set; } = default!;
    
    /// <summary>
    /// Farmers list
    /// </summary>
    public ICollection<Farmer> Farmers { get; set; } = default!;
    
    /// <summary>
    /// Medics list
    /// </summary>
    public ICollection<Medic> Medics { get; set; } = default!;
    
    /// <summary>
    /// Timbers list
    /// </summary>
    public ICollection<Timber> Timbers { get; set; } = default!;
    
    /// <summary>
    /// Traders list
    /// </summary>
    public ICollection<Trader> Traders { get; set; } = default!;
}