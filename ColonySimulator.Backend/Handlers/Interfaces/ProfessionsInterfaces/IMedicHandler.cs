using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;

/// <summary>
/// Handler for medics
/// </summary>
public interface IMedicHandler : IPersonActivity
{
    /// <summary>
    /// Handles healing
    /// </summary>
    /// <param name="medicine">medicine</param>
    /// <param name="person">Person</param>
    /// <param name="medLevel">level of entity</param>
    /// <returns></returns>
    public Task Heal(Medicine medicine, Person person, int medLevel);
}