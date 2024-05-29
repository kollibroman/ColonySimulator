using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;

public interface IMedicHandler : IPersonActivity
{
    public Task Heal(Medicine medicine, Person person, int medLevel);
}