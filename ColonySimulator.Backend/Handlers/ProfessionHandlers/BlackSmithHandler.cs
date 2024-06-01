using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;
using ColonySimulator.Backend.Persistence.Models.Threats;

namespace ColonySimulator.Backend.Handlers.ProfessionHandlers;

/// <summary>
/// Handler for blacksmith
/// </summary>
public class BlackSmithHandler : IBlackSmithHandler
{
    /// <summary>
    /// generate new weapon
    /// </summary>
    /// <param name="weapon">Weapon</param>
    /// <param name="wood">Wood</param>
    /// <param name="blackLevel">blacksmith level</param>
    /// <returns></returns>
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
    
    /// <summary>
    /// Experiences threat
    /// </summary>
    /// <param name="effect">effect of threat</param>
    /// <param name="proffesion">profession of entity</param>
    /// <param name="resources">resources lost</param>
    /// <returns></returns>
    public Task ExperienceThreat(Effect effect, Proffesion proffesion, List<Resource> resources)
    {
        if (effect.GetType() == typeof(FightingThreatEffect))
        {
            var fEffect = (FightingThreatEffect)effect;

            proffesion.Vitality -= fEffect.Damage;

            try
            {
                var weaponry = (Weaponry)resources.SingleOrDefault(x => x.GetType() == typeof(Weaponry))!;
                var weaponryEffect = (Weaponry)fEffect.ResourcesStolen.SingleOrDefault(x => x.GetType() == typeof(Weaponry))!;
                var wood = (Wood)resources.SingleOrDefault(x => x.GetType() == typeof(Wood))!;
                var woodEffect = (Wood)fEffect.ResourcesStolen.SingleOrDefault(x => x.GetType() == typeof(Wood))!;

                weaponry.WeaponryCount -= weaponryEffect.WeaponryCount;
                wood.WoodCount -= woodEffect.WoodCount;

            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        if (effect.GetType() == typeof(PlagueEffect))
        {
            var pEffect = (PlagueEffect)effect;

            proffesion.Vitality -= pEffect.Damage;
            proffesion.IsSick = pEffect.IsSick;
        }

        if (effect.GetType() == typeof(NaturalEffect))
        {
            var nEffect = (NaturalEffect)effect;

            proffesion.Vitality -= nEffect.Damage;
            proffesion.IsHungry = nEffect.IsHungry;
            
            try
            {
                var weaponry = (Weaponry)resources.SingleOrDefault(x => x.GetType() == typeof(Weaponry))!;
                var weaponryEffect = (Weaponry)nEffect.ResourcesLost.SingleOrDefault(x => x.GetType() == typeof(Weaponry))!;
                var wood = (Wood)resources.SingleOrDefault(x => x.GetType() == typeof(Wood))!;
                var woodEffect = (Wood)nEffect.ResourcesLost.SingleOrDefault(x => x.GetType() == typeof(Wood))!;

                weaponry.WeaponryCount -= weaponryEffect.WeaponryCount;
                wood.WoodCount -= woodEffect.WoodCount;
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        return Task.CompletedTask;
    }
}