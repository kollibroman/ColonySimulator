namespace ColonySimulator.Backend.Persistence.Models.Professions;

public class ProfessionsOverview
{
    public ICollection<Apothecary> Apothecaries { get; set; } = default!;
    public ICollection<BlackSmith> BlackSmiths { get; set; } = default!;
    public ICollection<Farmer> Farmers { get; set; } = default!;
    public ICollection<Medic> Medics { get; set; } = default!;
    public ICollection<Timber> Timbers { get; set; } = default!;
    public ICollection<Trader> Traders { get; set; } = default!;
}