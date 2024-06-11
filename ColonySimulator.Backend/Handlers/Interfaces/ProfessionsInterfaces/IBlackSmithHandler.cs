using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;

/// <summary>
/// Handler for blacksmiths
/// </summary>
public interface IBlackSmithHandler : IPersonActivity
{
    /// <summary>
    /// Handles weapon creation
    /// </summary>
    /// <param name="weapon">Weapon</param>
    /// <param name="wood">Wood</param>
    /// <param name="blackLevel">level of entity</param>
    /// <returns></returns>
    public Task CreateWeapon(Weaponry weapon, Wood wood, int blackLevel);
}