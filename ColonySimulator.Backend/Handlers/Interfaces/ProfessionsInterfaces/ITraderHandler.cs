using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;

/// <summary>
/// Handler for traders
/// </summary>
public interface ITraderHandler : IPersonActivity
{
    /// <summary>
    /// Handles trading
    /// </summary>
    /// <param name="crops">Crops</param>
    /// <param name="wood">Wood</param>
    /// <param name="medicine">Medicine</param>
    /// <param name="herbs">Herbs</param>
    /// <param name="weaponry">Weaponry</param>
    public Task Trade(Crops crops, Wood wood, Medicine medicine, Herbs herbs, Weaponry weaponry);
}