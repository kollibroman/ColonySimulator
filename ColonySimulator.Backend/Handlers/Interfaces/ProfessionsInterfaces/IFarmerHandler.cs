using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;

public interface IFarmerHandler : IPersonActivity
{
    public Task Farm(Crops crops, Herbs herbs, int farmLevel);
}