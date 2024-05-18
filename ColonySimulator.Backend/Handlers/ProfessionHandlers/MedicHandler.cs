using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Persistence.Models.Resources;
using ColonySimulator.Backend.Persistence.Models.Threats;

namespace ColonySimulator.Backend.Handlers.ProfessionHandlers;

public class MedicHandler : IMedicHandler
{
    public Task Heal(Medicine medicine)
    {
        throw new NotImplementedException();
    }
    
    public Task ExperienceThreat(Threat threat)
    {
        throw new NotImplementedException();
    }
}