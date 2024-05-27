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
        var rnd = new Random();
        var nameArr = new string[] { "Storm", "SnowStorm", "Snow", "Rain", "Flood", "Drought", "Hunger" };

        var naturalList = new List<NaturalThreat>();

        for (int i = 0; i <= 15; i++)
        {
            var threatLevel = rnd.Next(1, 5);
            var naturalThreat = new NaturalThreat
            {
                Name = nameArr[rnd.Next(0,6)],
                RequiredCropsCount = rnd.Next(_counter.PopulationCount/4,(_counter.PopulationCount/4)*threatLevel),
                ThreatLevel = threatLevel,
                RequiredFarmingLevel = rnd.Next(1,5)
            };
            
            naturalList.Add(naturalThreat);
        }

        await _dbContext.AddRangeAsync(naturalList, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task SeedFighting(CancellationToken ct)
    {
        var rnd = new Random();
        var nameArr = new string[] { "Bandits", "Riot", "Wolves", "Outcasts", "Army", "Demons" };

        var fightingList = new List<FightingThreat>();

        for (int i = 0; i <= 10; i++)
        {
            var threatLevel = rnd.Next(1, 5);
            var threat = new FightingThreat
            {
                Name = nameArr[rnd.Next(0, 5)],
                RequiredSmithingLevel = rnd.Next(1, 5),
                RequiredWeaponryCount =
                    rnd.Next(_counter.PopulationCount / 10, (_counter.PopulationCount / 10) * threatLevel),
                ThreatLevel = threatLevel
            };
            
            fightingList.Add(threat);
        }

        await _dbContext.AddRangeAsync(fightingList, ct);
        await _dbContext.SaveChangesAsync(ct);
    }
}