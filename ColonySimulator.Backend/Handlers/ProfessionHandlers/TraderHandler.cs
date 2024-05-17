using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Handlers.ProfessionHandlers;

public class TraderHandler : ITraderHandler
{
    public Task Trade(Wood wood, Medicine medicine, Herbs herbs, Weaponry weaponry)
    {
        throw new NotImplementedException();
    }
}