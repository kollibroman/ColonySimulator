using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;

public interface IFarmerHandler
{
    public Task Farm(Crops crops);
}