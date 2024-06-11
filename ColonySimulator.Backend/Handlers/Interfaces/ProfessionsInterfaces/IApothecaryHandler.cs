using ColonySimulator.Backend.Handlers.ProfessionHandlers;
using ColonySimulator.Backend.Persistence.Models.Resources;
using ColonySimulator.Backend.Persistence.Models.Threats;

namespace ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;

/// <summary>
/// Handler for apothecary
/// </summary>
public interface IApothecaryHandler :IPersonActivity
{
    /// <summary>
    /// Handles creating medicine
    /// </summary>
    /// <param name="herbs">Herbs</param>
    /// <param name="medicine">medicine</param>
    /// <param name="apoLevel">level of entity</param>
    /// <returns></returns>
    public Task CreateMedicine(Herbs herbs, Medicine medicine, int apoLevel);
    
    /// <summary>
    /// Handles herbs collection
    /// </summary>
    /// <param name="herbs">Herbs</param>
    /// <param name="apoLevel">level of entity</param>
    /// <returns></returns>
    public Task CollectingHerbs(Herbs herbs, int apoLevel);
}