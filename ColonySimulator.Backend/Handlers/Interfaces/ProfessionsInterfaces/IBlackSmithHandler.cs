using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;

public interface IBlackSmithHandler : IPersonActivity
{
    public Task CreateWeapon(Weaponry weapon, Wood wood);
}