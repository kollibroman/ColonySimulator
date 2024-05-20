using ColonySimulator.Backend.Helpers;
using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Persistence.Models.Threats;

namespace ColonySimulator.Backend.Seeders;

public class ThreatSeeder
{
    private readonly ColonySimulatorContext _dbContext;
    private readonly PopCounter _counter;

    public ThreatSeeder(ColonySimulatorContext dbContext, PopCounter counter)
    {
        _dbContext = dbContext;
        _counter = counter;
    }

    public async Task SeedPlagues(CancellationToken ct)
    {
        var rnd = new Random();
        var nameArr = new string[] { "Rabies", "Ligma", "Black Plague", "Blood Plague", "Red Plague", "Herpes" };
        
        var plagueList = new List<PlagueThreat>();

        for (int i = 0; i <= 10; i++)
        {
            var threatLevel = rnd.Next(1, 5);
            var plague = new PlagueThreat
            {
                Name = nameArr[rnd.Next(0, 5)],
                RequiredMedicalLevel = rnd.Next(1, 5),
                ThreatLevel = threatLevel,
                RequiredMedicineCount = rnd.Next(1, (_counter.PopulationCount/10)*threatLevel)
            };
            
            plagueList.Add(plague);
        }

        await _dbContext.AddRangeAsync(plagueList, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task SeedNatural(CancellationToken ct)
    {
        
    }

    public async Task SeedFighting(CancellationToken ct)
    {
        
    }
}