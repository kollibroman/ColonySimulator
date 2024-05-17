using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;

public interface ITraderHandler : IPersonActivity
{
    public Task Trade(Wood wood, Medicine medicine, Herbs herbs, Weaponry weaponry);
}