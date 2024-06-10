namespace ColonySimulator.Backend.Helpers;

/// <summary>
/// Population counter class
/// </summary>
public class PopCounter
{
    /// <summary>
    /// Actual number of people 
    /// </summary>
    public int PopulationCount { get; set; } = 0;

    /// <summary>
    /// Number of apothecaries
    /// </summary>
    public int ApothecariesCount { get; set; } = 0;
    
    /// <summary>
    /// Number of Blacksmiths
    /// </summary>
    public int BlackSmithCount { get; set; } = 0;
    
    /// <summary>
    /// Number of farmers
    /// </summary>
    public int FarmerCount { get; set; } = 0;
    
    /// <summary>
    /// Number of medics
    /// </summary>
    public int MedicCount { get; set; } = 0;
    
    /// <summary>
    /// Number of timbers
    /// </summary>
    public int TimberCount { get; set; } = 0;
    
    /// <summary>
    /// People lost during simulation
    /// </summary>
    public int PeopleLost { get; set; } = 0;
}