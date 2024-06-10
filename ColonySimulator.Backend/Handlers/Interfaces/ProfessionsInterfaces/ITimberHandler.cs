using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;

/// <summary>
/// Handler for timber
/// </summary>
public interface ITimberHandler : IPersonActivity
{
    /// <summary>
    /// Handles wood creation
    /// </summary>
    /// <param name="wood">Wood</param>
    /// <param name="timLevel">level of entity</param>
    public Task CreateWood(Wood wood, int timLevel);
}