namespace ColonySimulator.Backend.Helpers;

/// <summary>
/// Stores end data
/// </summary>
public class EndDataStorer
{
    // /// <summary>
    // /// Generic parameter to pass
    // /// </summary>
    // public ThreatsOverview ThreatsOverview { get; set; } = default!;
    
    /// <summary>
    /// Population number
    /// </summary>
    public int PopulationCount { get; set; }
    
    /// <summary>
    /// Number of people lost
    /// </summary>
    public int PopulationLost { get; set; }
    
    /// <summary>
    /// second generic parameter to pass
    /// </summary>
    public ProfessionsOverview ProfessionsOverview { get; set; } = default!;
    
    /// <summary>
    /// Resource overview
    /// </summary>
    public ResourceOverview ResourceOverview { get; set; } = default!;
}