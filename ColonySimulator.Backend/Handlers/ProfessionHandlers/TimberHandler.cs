using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Handlers.ProfessionHandlers;

/// <summary>
/// Handler for timbers
/// </summary>
public class TimberHandler : ITimberHandler
{
    /// <summary>
    /// Creates wood
    /// </summary>
    /// <param name="wood">entity to modify</param>
    /// <param name="timLevel">level of timber</param>
    /// <returns></returns>
    public Task CreateWood(Wood wood, int timLevel)
    {
        wood.WoodCount += 2 * timLevel;
        return Task.CompletedTask;
    }
    
    /// <summary>
    /// Threat experience
    /// </summary>
    /// <param name="effect">effect of threat</param>
    /// <param name="proffesion">Profession of entity</param>
    /// <param name="resources">List of affected resources</param>
    /// <returns>Completed task</returns>
    public Task ExperienceThreat(Effect effect, Proffesion proffesion, List<Resource> resources)
    {
        switch (effect)
        {
            case FightingThreatEffect fEffect:
                proffesion.Vitality -= fEffect.Damage;

                try
                {
                    var wood = (Wood)resources.SingleOrDefault(x => x.GetType() == typeof(Wood))!;
                    var woodEffect = (Wood)fEffect.ResourcesStolen.SingleOrDefault(x => x.GetType() == typeof(Wood))!;

                    if (wood.WoodCount - woodEffect.WoodCount >= 0)
                    {
                        wood.WoodCount -= woodEffect.WoodCount;
                    }
                    else
                    {
                        wood.WoodCount = 0;
                    }
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                break;
            case PlagueEffect pEffect:
                proffesion.Vitality -= pEffect.Damage;
                proffesion.IsSick = pEffect.IsSick;
                break;
            case NaturalEffect nEffect:
                proffesion.Vitality -= nEffect.Damage;
                proffesion.IsHungry = nEffect.IsHungry;
            
                try
                {
                    var wood = (Wood)resources.SingleOrDefault(x => x.GetType() == typeof(Wood))!;
                    var woodEffect = (Wood)nEffect.ResourcesLost.SingleOrDefault(x => x.GetType() == typeof(Wood))!;

                    if (wood.WoodCount - woodEffect.WoodCount >= 0)
                    {
                        wood.WoodCount -= woodEffect.WoodCount;
                    }
                    else
                    {
                        wood.WoodCount = 0;
                    }
                }
                catch (ArgumentNullException e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                break;
        }

        return Task.CompletedTask;
    }
}