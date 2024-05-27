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
    /// People lost during simulation
    /// </summary>
    public int PeopleLost { get; set; } = 0;
}