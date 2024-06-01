using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;
using ColonySimulator.Backend.Persistence.Models.Threats;

namespace ColonySimulator.Backend.Handlers.Interfaces;

/// <summary>
/// Main person activity interface
/// </summary>

public interface IPersonActivity
{
    /// <summary>
    /// Experience threat
    /// </summary>
    /// <param name="effect">Experienced threat effect</param>
    /// <param name="profession">Profession of entity</param>
    /// <param name="resources">resources to modify</param>
    /// <returns></returns>
    public Task ExperienceThreat(Effect effect, Proffesion profession, List<Resource> resources);
}