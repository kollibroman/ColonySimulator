using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Persistence.Models.Resources;
using ColonySimulator.Backend.Persistence.Models.Threats;

namespace ColonySimulator.Backend.Handlers.ProfessionHandlers;

public class FarmerHandler : IFarmerHandler
{
    public Task Farm(Crops crops)
    {
        throw new NotImplementedException();
    }
    public Task ExperienceThreat(Threat threat)
    {
        throw new NotImplementedException();
    }
}