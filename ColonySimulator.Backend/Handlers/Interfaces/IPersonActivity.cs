using ColonySimulator.Backend.Persistence.Models.Threats;

namespace ColonySimulator.Backend.Handlers.Interfaces;

public interface IPersonActivity
{
    public Task ExperienceThreat(Threat threat);
}