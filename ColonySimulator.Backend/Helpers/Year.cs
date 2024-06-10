namespace ColonySimulator.Backend.Helpers;

/// <summary>
/// Year counter class
/// </summary>
public class Year
{
    /// <summary>
    /// Current year of simulation
    /// </summary>
    public int YearOfSim { get; set; } = 0;
    
    /// <summary>
    /// Duration of simulation
    /// </summary>
    public int SimDuration { get; set; } = 0;
}