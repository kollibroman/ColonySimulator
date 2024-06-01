using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Handlers.ProfessionHandlers;

/// <summary>
/// Handler for medic
/// </summary>
public class MedicHandler : IMedicHandler
{
    /// <summary>
    /// heal given person
    /// </summary>
    /// <param name="medicine">medicine entity used</param>
    /// <param name="person">person affected</param>
    /// <param name="medLevel">level of entity</param>
    /// <returns></returns>
    public Task Heal(Medicine medicine, Person person, int medLevel)
    {
        if(medicine.MedicineCount - 5/medLevel < 0){}
        else
        {
            person.IsSick = false;
            medicine.MedicineCount -= 5 / medLevel;
        }

        return Task.CompletedTask;
    }
    
    /// <summary>
    /// Experienced threat by entity
    /// </summary>
    /// <param name="effect">effect of threat</param>
    /// <param name="proffesion">entity's profession</param>
    /// <param name="resources">resources affected</param>
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

                medicine.MedicineCount -= medicineEffect.MedicineCount;
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
                var medicineEffect = (Medicine)nEffect.ResourcesLost.SingleOrDefault(x => x.GetType() == typeof(Medicine))!;

                medicine.MedicineCount -= medicineEffect.MedicineCount;
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