using ColonySimulator.Backend.Persistence.Models.Professions;

namespace ColonySimulator.Backend.Helpers;

public class ConsequenceOverview<T, TR>
{
    public ConsequenceOverview()
    {
    }

    public ConsequenceOverview(T parameters, TR professionsOverview)
    {
        Parameters = parameters;
        PopulationCount = PopCounter.PopulationCout;
        PopulationLost = PopCounter.PeopleLost;
        ProfessionsOverview = professionsOverview;
    }

    public T Parameters { get; set; }
    public int PopulationCount { get; set; }
    public int PopulationLost { get; set; }
    public TR ProfessionsOverview { get; set; }
}