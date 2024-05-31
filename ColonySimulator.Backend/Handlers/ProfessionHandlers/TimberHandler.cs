using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Persistence.Models.Resources;
using ColonySimulator.Backend.Persistence.Models.Threats;

namespace ColonySimulator.Backend.Handlers.ProfessionHandlers;

public class TimberHandler : ITimberHandler
{
    public Task CreateWood(Wood wood, int timLevel)
    {
        wood.WoodCount += 2 * timLevel;
        return Task.CompletedTask;
    }
    
    public Task ExperienceThreat(Effect effect)
    {
        throw new NotImplementedException();
    }
}