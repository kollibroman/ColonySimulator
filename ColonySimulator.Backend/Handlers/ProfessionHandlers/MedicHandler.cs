using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Handlers.ProfessionHandlers;

public class MedicHandler : IMedicHandler
{
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
    
    public Task ExperienceThreat(Effect effect)
    {
        throw new NotImplementedException();
    }
}