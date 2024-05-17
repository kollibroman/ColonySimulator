using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;

public interface ITimberHandler : IPersonActivity
{
    public Task CreateWood(Wood wood);
}