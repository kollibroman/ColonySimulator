using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Persistence.Models.Resources;
using ColonySimulator.Backend.Persistence.Models.Threats;

namespace ColonySimulator.Backend.Handlers.ProfessionHandlers;

public class ApothecaryHandler : IApothecaryHandler
{
    public Task CreateMedicine(Herbs herbs, Medicine medicine, int apoLevel)
    {
        //Making sure herbs don't go under 0
        if (herbs.HerbsCount - 6 <= 0){}
        else
        {
            medicine.MedicineCount += 1 * apoLevel;
            herbs.HerbsCount -= 6;
        }
        
        return Task.CompletedTask;
    }

    public Task CollectingHerbs(Herbs herbs, int apoLevel)
    {
        herbs.HerbsCount += 2 * apoLevel;
        return Task.CompletedTask;
    }

    public Task ExperienceThreat(Threat threat)
    {
        throw new NotImplementedException();
    }
}
