using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;
using ColonySimulator.Backend.Persistence.Models.Threats;

namespace ColonySimulator.Backend.Handlers.Interfaces;

/// <summary>
/// Handles threats
/// </summary>
public interface IThreatHandler
{
    /// <summary>
    /// Calculates entity's affection of threat
    /// </summary>
    /// <param name="profession">entities profession</param>
    /// <param name="threat">Threat on which calculation is made</param>
    /// <returns></returns>
    public Task CalculateAffection(Proffesion profession, Threat threat);

    /// <summary>
    /// Generates effect of threat based on calculated affection
    /// </summary>
    /// <param name="threat">Passed threat</param>
    /// <param name="resources"></param>
    /// <returns>Generated effect</returns>
    public Task<Effect> GenerateEffects(Threat threat, List<Resource> resources);
    
    /// <summary>
    /// Generates random threat
    /// </summary>
    /// <returns>Randomly generated threat</returns>
    public Task<Threat?> GenerateRandomThreat(CancellationToken ct);
    
    /// <summary>
    /// Calculates affected resources
    /// </summary>
    /// <param name="resources">List of resources to pass</param>
    /// <param name="threat">threat affecting</param>
    /// <returns>list of affected resources</returns>
    public List<Resource> CalculateUsedResources(List<Resource> resources, Threat threat);
}