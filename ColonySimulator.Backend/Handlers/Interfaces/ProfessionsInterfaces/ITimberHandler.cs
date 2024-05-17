using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;

public interface ITimberHandler
{
    public Task CreateWood(Wood wood);
}