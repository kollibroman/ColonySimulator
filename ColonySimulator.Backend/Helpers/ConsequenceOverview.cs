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
        DeadPeople = PopCounter.DeadPeople;
        ProfessionsOverview = professionsOverview;
    }

    public T Parameters { get; set; }
    public int PopulationCount { get; set; }
    public int DeadPeople { get; set; }
    public TR ProfessionsOverview { get; set; }
}