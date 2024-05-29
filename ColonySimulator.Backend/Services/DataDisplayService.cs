using System.Text.Json;
using ColonySimulator.Backend.Helpers;
using ColonySimulator.Backend.Persistence.Models.Professions;
using Newtonsoft.Json;

namespace ColonySimulator.Backend.Services;
/// <summary>
/// Class to display data and serialize it
/// </summary>
public class DataDisplayService
{
    private readonly PopCounter _counter;

    /// <summary>
    /// constructor for it with DI parameters
    /// </summary>
    /// <param name="counter">Population counter class</param>
    public DataDisplayService(PopCounter counter)
    {
        _counter = counter;
    }
    /// <summary>
    /// Serialize and display objects in a pleasant way
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TR"></typeparam>
    /// <returns>Serialized string based on passed content</returns>
    public string SerializeAndDisplayData<T,TR>(T professionsOverview, TR threatsOverview, ResourceOverview resourceOverview) where T : ProfessionsOverview where TR : ThreatsOverview
    {
        var overview = new ConsequenceOverview<T, TR>(professionsOverview, threatsOverview, _counter.PopulationCount, _counter.PeopleLost, resourceOverview);
        var serializedString = JsonConvert.SerializeObject(overview, Formatting.Indented);

        return serializedString;
    }
}