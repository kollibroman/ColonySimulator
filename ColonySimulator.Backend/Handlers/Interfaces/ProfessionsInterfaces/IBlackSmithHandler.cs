using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;

public interface IBlackSmithHandler
{
    public Task CreateWeapon(Weaponry weapon, Wood wood);
}