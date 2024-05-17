using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Persistence.Models.Resources;
using ColonySimulator.Backend.Persistence.Models.Threats;

namespace ColonySimulator.Backend.Handlers.ProfessionHandlers;

public class TraderHandler : ITraderHandler
{
    public Task Trade(Wood wood, Medicine medicine, Herbs herbs, Weaponry weaponry)
    {
        throw new NotImplementedException();
    }
    
    public Task ExperienceThreat(Threat threat)
    {
        throw new NotImplementedException();
    }
}