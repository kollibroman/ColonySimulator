using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Persistence.Models.Professions;

namespace ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;

public interface IProfessionHandler : IPersonActivity
{
    public Task DoWork(Proffesion proffesion);
    public Task ExperienceThreat(Effect effect);
}