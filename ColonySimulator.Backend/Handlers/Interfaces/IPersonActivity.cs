using ColonySimulator.Backend.Effects;
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
    /// <returns></returns>
    public Task ExperienceThreat(Effect effect);
}