using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Threats;

namespace ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;

/// <summary>
/// Handles professions
/// </summary>
public interface IProfessionHandler
{
    Task HandleFarm();
    Task HandleApothecary();
    Task HandleTimber();
    Task HandleBlackSmith();
    Task HandleTrader();
    Task HandleMedic();

    public Threat ThreatToExperience { get; set; }
}