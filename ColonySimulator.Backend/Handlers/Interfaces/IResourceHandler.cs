using ColonySimulator.Backend.Persistence.Models.Professions;

namespace ColonySimulator.Backend.Handlers.Interfaces;

public interface IResourceHandler
{
    public Task ConsumeResources(int popCount);
}