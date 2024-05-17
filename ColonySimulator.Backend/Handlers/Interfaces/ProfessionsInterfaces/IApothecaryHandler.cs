using ColonySimulator.Backend.Persistence.Models.Resources;
using ColonySimulator.Backend.Persistence.Models.Threats;

namespace ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;

public interface IApothecaryHandler
{
    public Task CreateMedicine(Herbs herbs);
    public Task CollectingHerbs(Herbs herbs);
    public Task ThreatExperience(Threat threat);
}