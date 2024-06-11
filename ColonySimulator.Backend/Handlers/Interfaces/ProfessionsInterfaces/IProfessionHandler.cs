using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Threats;

namespace ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;

/// <summary>
/// Handles professions
/// </summary>
public interface IProfessionHandler
{
    /// <summary>
    /// Handles farmers
    /// </summary>
    Task HandleFarm();
    
    /// <summary>
    /// Handles apothecaries
    /// </summary>
    Task HandleApothecary();
    
    /// <summary>
    /// Handles timbers
    /// </summary>
    Task HandleTimber();
    
    /// <summary>
    /// Handles blacksmiths
    /// </summary>
    Task HandleBlackSmith();
    
    /// <summary>
    /// Handles trader
    /// </summary>
    Task HandleTrader();
    
    /// <summary>
    /// Handles medics
    /// </summary>
    /// <returns></returns>
    Task HandleMedic();
}