using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;
using ColonySimulator.Backend.Persistence.Models.Threats;

namespace ColonySimulator.Backend.Handlers.ProfessionHandlers;

/// <summary>
/// Handles apothecary activity
/// </summary>
public class ApothecaryHandler : IApothecaryHandler
{
    /// <summary>
    /// Creates medicine
    /// </summary>
    /// <param name="herbs">Modifies herbs entity</param>
    /// <param name="medicine">Modifies medicine entity</param>
    /// <param name="apoLevel">level of apothecary</param>
    /// <returns></returns>
    public Task CreateMedicine(Herbs herbs, Medicine medicine, int apoLevel)
    {
        if (herbs.HerbsCount != 0)
        {
            if (herbs.HerbsCount - 6 <= 0)
            {
                medicine.MedicineCount += 0;
                herbs.HerbsCount -= 0;
            }
            else
            {
                medicine.MedicineCount += 1 * apoLevel;
                herbs.HerbsCount -= 6;
            }
        }
        
        return Task.CompletedTask;
    }
    
    /// <summary>
    /// Generates Herbs
    /// </summary>
    /// <param name="herbs">herbs to modify</param>
    /// <param name="apoLevel">level of apothecary</param>
    /// <returns></returns>
    public Task CollectingHerbs(Herbs herbs, int apoLevel)
    {
        herbs.HerbsCount += 2 * apoLevel;
        return Task.CompletedTask;
    }
    
    /// <summary>
    /// Experience threat as entity
    /// </summary>
    /// <param name="effect">effect of threat</param>
    /// <param name="proffesion">profession of entity</param>
    /// <param name="resources">resources to modify</param>
    /// <returns></returns>
    public Task ExperienceThreat(Effect effect, Proffesion proffesion, List<Resource> resources)
    {
        if (effect.GetType() == typeof(FightingThreatEffect))
        {
            var fEffect = (FightingThreatEffect)effect;

            proffesion.Vitality -= fEffect.Damage;

            try
            {
                var medicine = (Medicine)resources.SingleOrDefault(x => x.GetType() == typeof(Medicine))!;
                var medicineEffect = (Medicine)fEffect.ResourcesStolen.SingleOrDefault(x => x.GetType() == typeof(Medicine))!;
                var herbs = (Herbs)resources.SingleOrDefault(x => x.GetType() == typeof(Herbs))!;
                var herbsEffect = (Herbs)fEffect.ResourcesStolen.SingleOrDefault(x => x.GetType() == typeof(Herbs))!;

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
                var medicine = (Medicine)resources.SingleOrDefault(x => x.GetType() == typeof(Medicine))!;
                var medicineEffect = (Medicine)nEffect.ResourcesLost?.SingleOrDefault(x => x.GetType() == typeof(Medicine))!;
                var herbs = (Herbs)resources.SingleOrDefault(x => x.GetType() == typeof(Herbs))!;
                var herbsEffect = (Herbs)nEffect.ResourcesLost?.SingleOrDefault(x => x.GetType() == typeof(Herbs))!;

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
