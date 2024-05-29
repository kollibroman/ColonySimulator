using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Persistence.Models.Resources;
using ColonySimulator.Backend.Persistence.Models.Threats;

namespace ColonySimulator.Backend.Handlers.ProfessionHandlers;

public class FarmerHandler : IFarmerHandler
{
    public Task Farm(Crops crops, Herbs herbs, int farmLevel)
    {
        crops.CropsCount += 3 * farmLevel;
        herbs.HerbsCount += 1 * farmLevel;
        return Task.CompletedTask;
    }
    public Task ExperienceThreat(Threat threat)
    {
        throw new NotImplementedException();
    }
    
}