using ColonySimulator.Backend.Persistence.Models.Professions;

namespace ColonySimulator.Backend.Handlers.Interfaces;

/// <summary>
/// Handler for resources
/// </summary>
public interface IResourceHandler
{
    /// <summary>
    /// Handles resource consumption
    /// </summary>
    /// <param name="popCount">Population counter</param>
    /// <returns>Completed task</returns>
    public Task ConsumeResources(int popCount);
}