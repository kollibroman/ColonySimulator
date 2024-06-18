namespace ColonySimulator.Backend.Helpers;

public class RandomSeedingData
{
    /// <summary>
    /// Duration of simulation
    /// </summary>
    public required int Duration { get; set; } = 0;

    /// <summary>
    /// Simulation data
    /// </summary>
    public required SimulationData Data { get; set; } = new SimulationData();
}