using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Handlers.Interfaces;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Threats;

namespace ColonySimulator.Backend.Handlers;

public class Threathandler : IThreatHandler
{
    
    public Task CalculateAffection(Proffesion proffesion)
    {
        throw new NotImplementedException();
    }

    public Task<Effect> GenerateEffects(Threat threat)
    {
        throw new NotImplementedException();
    }
}