using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Handlers.ProfessionHandlers;

/// <summary>
/// Handle farmer
/// </summary>
public class FarmerHandler : IFarmerHandler
{
    /// <summary>
    /// Generate new resources
    /// </summary>
    /// <param name="crops">crops</param>
    /// <param name="herbs">herbs</param>
    /// <param name="farmLevel">level of entity</param>
    /// <returns></returns>
    public Task Farm(Crops crops, Herbs herbs, int farmLevel)
    {
        crops.CropsCount += 2 * farmLevel;
        herbs.HerbsCount += 2 * farmLevel;
        return Task.CompletedTask;
    }
    
    /// <summary>
    /// Experience threat
    /// </summary>
    /// <param name="effect">effect of threat</param>
    /// <param name="proffesion">profession</param>
    /// <param name="resources">resources affected</param>
    /// <returns></returns>
    public Task ExperienceThreat(Effect effect, Proffesion proffesion, List<Resource> resources)
    {
        switch (effect)
        {
            case FightingThreatEffect fEffect:
                proffesion.Vitality -= fEffect.Damage;

                try
                {
                    var crops = (Crops)resources.SingleOrDefault(x => x.GetType() == typeof(Crops))!;
                    var cropsEffect = (Crops)fEffect.ResourcesStolen?.SingleOrDefault(x => x.GetType() == typeof(Crops))!;
                    var herbs = (Herbs)resources.SingleOrDefault(x => x.GetType() == typeof(Herbs))!;
                    var herbsEffect = (Herbs)fEffect.ResourcesStolen?.SingleOrDefault(x => x.GetType() == typeof(Herbs))!;

                    if (crops.CropsCount - cropsEffect.CropsCount >= 0)
                    {
                        crops.CropsCount -= cropsEffect.CropsCount;
                    }
                    else
                    {
                        crops.CropsCount = 0;
                    }

                    if (herbs.HerbsCount - herbsEffect.HerbsCount >= 0)
                    {
                        herbs.HerbsCount -= herbsEffect.HerbsCount;    
                    }
                    else
                    {
                        herbs.HerbsCount = 0;
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
                    var crops = (Crops)resources.SingleOrDefault(x => x.GetType() == typeof(Crops))!;
                    var cropsEffect = (Crops)nEffect.ResourcesLost?.SingleOrDefault(x => x.GetType() == typeof(Crops))!;
                    var herbs = (Herbs)resources.SingleOrDefault(x => x.GetType() == typeof(Herbs))!;
                    var herbsEffect = (Herbs)nEffect.ResourcesLost?.SingleOrDefault(x => x.GetType() == typeof(Herbs))!;

                    if (crops.CropsCount - cropsEffect.CropsCount >= 0)
                    {
                        crops.CropsCount -= cropsEffect.CropsCount;
                    }
                    else
                    {
                        crops.CropsCount = 0;
                    }

                    if (herbs.HerbsCount - herbsEffect.HerbsCount >= 0)
                    {
                        herbs.HerbsCount -= herbsEffect.HerbsCount;    
                    }
                    else
                    {
                        herbs.HerbsCount = 0;
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