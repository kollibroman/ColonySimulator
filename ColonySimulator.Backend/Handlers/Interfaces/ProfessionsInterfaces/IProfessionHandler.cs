using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Persistence.Models.Professions;

namespace ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;

public interface IProfessionHandler
{
    Task HandleFarm();
    Task HandleApothecary();
    
}