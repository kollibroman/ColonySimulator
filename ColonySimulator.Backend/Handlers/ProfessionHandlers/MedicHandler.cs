﻿using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;
using ColonySimulator.Backend.Persistence.Models.Threats;

namespace ColonySimulator.Backend.Handlers.ProfessionHandlers;

public class MedicHandler : IMedicHandler
{
    public Task Heal(Medicine medicine, Person person, int medLevel)
    {
        if(medicine.MedicineCount - 1 < 0){}
        else
        {
            person.IsSick = false;
            medicine.MedicineCount -= 5 / medLevel;
        }

        return Task.CompletedTask;
    }
    
    public Task ExperienceThreat(Threat threat)
    {
        throw new NotImplementedException();
    }
}