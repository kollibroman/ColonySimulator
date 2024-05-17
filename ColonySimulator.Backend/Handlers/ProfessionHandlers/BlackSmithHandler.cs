using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Handlers.ProfessionHandlers;

public class BlackSmithHandler : IBlackSmithHandler
{
    public Task CreateWeapon(Weaponry weapon, Wood wood)
    {
        throw new NotImplementedException();
    }
}