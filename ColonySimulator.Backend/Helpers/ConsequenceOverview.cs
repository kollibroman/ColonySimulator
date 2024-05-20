using ColonySimulator.Backend.Persistence.Models.Professions;

namespace ColonySimulator.Backend.Helpers;

public class ConsequenceOverview<T, TR>
{
    public ConsequenceOverview()
    {
    }

    public ConsequenceOverview(T parameters, TR professionsOverview, int populationCount, int peopleLost)
    {
        Parameters = parameters;
        PopulationCount = populationCount;
        PopulationLost = peopleLost;
        ProfessionsOverview = professionsOverview;
    }

    public T Parameters { get; set; }
    public int PopulationCount { get; set; }
    public int PopulationLost { get; set; }
    public TR ProfessionsOverview { get; set; }
}