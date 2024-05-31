using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Persistence.Models.Resources;
using ColonySimulator.Backend.Persistence.Models.Threats;

namespace ColonySimulator.Backend.Handlers.ProfessionHandlers;

public class BlackSmithHandler : IBlackSmithHandler
{
    public Task CreateWeapon(Weaponry weapon, Wood wood, int blackLevel)
    {
        if (wood.WoodCount - 4 <= 0){}
        else
        {
            weapon.WeaponryCount += 1 * blackLevel;
            wood.WoodCount -= 4;
        }
        
        return Task.CompletedTask;
    }
    
    public Task ExperienceThreat(Effect effect)
    {
        throw new NotImplementedException();
    }
}