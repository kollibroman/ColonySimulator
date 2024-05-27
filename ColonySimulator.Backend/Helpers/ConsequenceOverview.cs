using ColonySimulator.Backend.Persistence.Models.Professions;

namespace ColonySimulator.Backend.Helpers;

/// <summary>
/// generic class to map data overview 
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TR"></typeparam>

public class ConsequenceOverview<T, TR>
{
    /// <summary>
    /// empty constructor
    /// </summary>
    public ConsequenceOverview()
    {
    }
    
    /// <summary>
    /// Constructor with generic parameters
    /// </summary>
    /// <param name="parameters">threat overview</param>
    /// <param name="professionsOverview">Professions overview class</param>
    
    public ConsequenceOverview(T parameters, TR professionsOverview)
    {
        Parameters = parameters;
        ProfessionsOverview = professionsOverview;
        PopulationCount = 0;
        PopulationLost = 0;
    }
    
    /// <summary>
    /// Main constructor with all needed parameters to show
    /// </summary>
    /// <param name="parameters">Threat overview class</param>
    /// <param name="professionsOverview">Professions overview class</param>
    /// <param name="populationCount">Actual population count</param>
    /// <param name="peopleLost">Number of people lost</param>
    public ConsequenceOverview(T parameters, TR professionsOverview, int populationCount, int peopleLost)
    {
        Parameters = parameters;
        PopulationCount = populationCount;
        PopulationLost = peopleLost;
        ProfessionsOverview = professionsOverview;
    }
    
    /// <summary>
    /// Generic parameter to pass
    /// </summary>
    public T Parameters { get; set; } = default!;
    
    /// <summary>
    /// Population number
    /// </summary>
    public int PopulationCount { get; set; }
    
    /// <summary>
    /// Number of people lost
    /// </summary>
    public int PopulationLost { get; set; }
    
    /// <summary>
    /// second generic parameter to pass
    /// </summary>
    public TR ProfessionsOverview { get; set; } = default!;
}