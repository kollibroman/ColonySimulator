using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Threats;

namespace ColonySimulator.Backend.Handlers.Interfaces;

public interface IThreatHandler
{
    public Task CalculateAffection(Proffesion proffesion);
    public Task<Effect> GenerateEffects(Threat threat);
}