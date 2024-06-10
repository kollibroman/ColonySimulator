using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;
using Spectre.Console;

namespace ColonySimulator.Backend.Handlers.ProfessionHandlers;

/// <summary>
/// Trader handler
/// </summary>
public class TraderHandler : ITraderHandler
{
    
    
    
    
    /// <summary>
    /// Generate new resources by trader
    /// </summary>
    /// <param name="crops">Crops</param>
    /// <param name="wood">Wood</param>
    /// <param name="medicine">Medicine</param>
    /// <param name="herbs">Herbs</param>
    /// <param name="weaponry">Weaponry</param>
    /// <returns>Completed task</returns>
    public Task Trade(Crops crops, Wood wood, Medicine medicine, Herbs herbs, Weaponry weaponry)
    {
        var resourcesCount = new List<int>
        {
            crops.CropsCount,
            wood.WoodCount,
            medicine.MedicineCount,
            herbs.HerbsCount,
            weaponry.WeaponryCount
        };

        var indexMax = resourcesCount.IndexOf(resourcesCount.Max());
        var indexMin = resourcesCount.IndexOf(resourcesCount.Min());

        var amountTraded = (int)Math.Ceiling(resourcesCount[indexMax] * 0.7);
        
        resourcesCount[indexMax] -= amountTraded;
        resourcesCount[indexMin] += amountTraded;

        return Task.CompletedTask;
    }
    
    /// <summary>
    /// Threat experience for trader
    /// </summary>
    /// <param name="effect">threat effect</param>
    /// <param name="proffesion">profession of entity</param>
    /// <param name="resources">resources affected</param>
    /// <returns>completed task</returns>
    public Task ExperienceThreat(Effect effect, Proffesion proffesion, List<Resource> resources)
    {
        if (effect.GetType() != typeof(PlagueEffect))
        {
            Medicine? medicine;
            Herbs? herbs;
            Wood? wood;
            Crops? crops;
            Weaponry? weaponry;
            Medicine? medicineEffect;
            Herbs? herbsEffect;
            Wood? woodEffect;
            Crops? cropsEffect;
            Weaponry? weaponryEffect;
            
            switch (effect)
            {
                case FightingThreatEffect fEffect:
                    //Get resources from db
                    medicine = (Medicine)resources.SingleOrDefault(x => x.GetType() == typeof(Medicine))!;
                    herbs = (Herbs)resources.SingleOrDefault(x => x.GetType() == typeof(Herbs))!;
                    wood = (Wood)resources.SingleOrDefault(x => x.GetType() == typeof(Wood))!;
                    crops = (Crops)resources.SingleOrDefault(x => x.GetType() == typeof(Crops))!;
                    weaponry = (Weaponry)resources.SingleOrDefault(x => x.GetType() == typeof(Weaponry))!;
                    
                    medicineEffect = (Medicine)fEffect.ResourcesStolen.SingleOrDefault(x => x.GetType() == typeof(Medicine))!;
                    herbsEffect = (Herbs)fEffect.ResourcesStolen.SingleOrDefault(x => x.GetType() == typeof(Herbs))!;
                    woodEffect = (Wood)fEffect.ResourcesStolen.SingleOrDefault(x => x.GetType() == typeof(Wood))!;
                    cropsEffect = (Crops)fEffect.ResourcesStolen.SingleOrDefault(x => x.GetType() == typeof(Crops))!;
                    weaponryEffect = (Weaponry)fEffect.ResourcesStolen.SingleOrDefault(x => x.GetType() == typeof(Weaponry))!;

                    if (medicine.MedicineCount - medicineEffect.MedicineCount >= 0)
                    {
                        medicine.MedicineCount -= medicineEffect.MedicineCount;
                    }
                    else
                    {
                        medicine.MedicineCount = 0;
                    }

                    if (herbs.HerbsCount - herbsEffect.HerbsCount >= 0)
                    {
                        herbs.HerbsCount -= herbsEffect.HerbsCount;
                    }
                    else
                    {
                        herbs.HerbsCount = 0;
                    }

                    if (wood.WoodCount - woodEffect.WoodCount >= 0)
                    {
                        wood.WoodCount -= woodEffect.WoodCount;
                    }
                    else
                    {
                        wood.WoodCount = 0;
                    }

                    if (crops.CropsCount - cropsEffect.CropsCount >= 0)
                    {
                        crops.CropsCount -= cropsEffect.CropsCount;
                    }
                    else
                    {
                        crops.CropsCount = 0;
                    }

                    if (weaponry.WeaponryCount - weaponryEffect.WeaponryCount >= 0)
                    {
                        weaponry.WeaponryCount -= weaponryEffect.WeaponryCount;
                    }
                    else
                    {
                        weaponry.WeaponryCount = 0;
                    }
                    break;
                
                case NaturalEffect nEffect:
                    //Get resources from db
                    medicine = (Medicine)resources.SingleOrDefault(x => x.GetType() == typeof(Medicine))!;
                    herbs = (Herbs)resources.SingleOrDefault(x => x.GetType() == typeof(Herbs))!;
                    wood = (Wood)resources.SingleOrDefault(x => x.GetType() == typeof(Wood))!;
                    crops = (Crops)resources.SingleOrDefault(x => x.GetType() == typeof(Crops))!;
                    weaponry = (Weaponry)resources.SingleOrDefault(x => x.GetType() == typeof(Weaponry))!;
                    
                    medicineEffect = (Medicine)nEffect.ResourcesLost.SingleOrDefault(x => x.GetType() == typeof(Medicine))!;
                    herbsEffect = (Herbs)nEffect.ResourcesLost.SingleOrDefault(x => x.GetType() == typeof(Herbs))!;
                    woodEffect = (Wood)nEffect.ResourcesLost.SingleOrDefault(x => x.GetType() == typeof(Wood))!;
                    cropsEffect = (Crops)nEffect.ResourcesLost.SingleOrDefault(x => x.GetType() == typeof(Crops))!;
                    weaponryEffect = (Weaponry)nEffect.ResourcesLost.SingleOrDefault(x => x.GetType() == typeof(Weaponry))!;
                    
                    if (medicine.MedicineCount - medicineEffect.MedicineCount >= 0)
                    {
                        medicine.MedicineCount -= medicineEffect.MedicineCount;
                    }
                    else
                    {
                        medicine.MedicineCount = 0;
                    }

                    if (herbs.HerbsCount - herbsEffect.HerbsCount >= 0)
                    {
                        herbs.HerbsCount -= herbsEffect.HerbsCount;
                    }
                    else
                    {
                        herbs.HerbsCount = 0;
                    }

                    if (wood.WoodCount - woodEffect.WoodCount >= 0)
                    {
                        wood.WoodCount -= woodEffect.WoodCount;
                    }
                    else
                    {
                        wood.WoodCount = 0;
                    }

                    if (crops.CropsCount - cropsEffect.CropsCount >= 0)
                    {
                        crops.CropsCount -= cropsEffect.CropsCount;
                    }
                    else
                    {
                        crops.CropsCount = 0;
                    }

                    if (weaponry.WeaponryCount - weaponryEffect.WeaponryCount >= 0)
                    {
                        weaponry.WeaponryCount -= weaponryEffect.WeaponryCount;
                    }
                    else
                    {
                        weaponry.WeaponryCount = 0;
                    }
                    break;
            }
        }

        return Task.CompletedTask;
    }
}