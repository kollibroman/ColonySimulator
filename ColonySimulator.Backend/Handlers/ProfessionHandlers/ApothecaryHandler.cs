using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Persistence.Models.Resources;
using ColonySimulator.Backend.Persistence.Models.Threats;

namespace ColonySimulator.Backend.Handlers.ProfessionHandlers;

public class ApothecaryHandler : IApothecaryHandler
{
    public Task CreateMedicine(Herbs herbs)
    {
        throw new NotImplementedException();
    }

    public Task CollectingHerbs(Herbs herbs)
    {
        throw new NotImplementedException();
    }

    public Task ThreatExperience(Threat threat)
    {
        throw new NotImplementedException();
    }
}
