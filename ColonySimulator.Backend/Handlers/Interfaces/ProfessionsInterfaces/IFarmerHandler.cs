using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;

/// <summary>
/// Handler for farmers
/// </summary>
public interface IFarmerHandler : IPersonActivity
{
    /// <summary>
    /// Handles farming
    /// </summary>
    /// <param name="crops">Crops</param>
    /// <param name="herbs">herbs</param>
    /// <param name="farmLevel">level of entity</param>
    /// <returns></returns>
    public Task Farm(Crops crops, Herbs herbs, int farmLevel);
}