namespace ColonySimulator.Backend.Helpers;

/// <summary>
/// Stores input data for simulation
/// </summary>
public class InputStorer
{
    /// <summary>
    /// Duration of simulation
    /// </summary>
    public required int Duration { get; init; }
    
    /// <summary>
    /// Simulation data
    /// </summary>
    public required SimulationData Data { get; init; }
}