using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Persistence.Enums;
using ColonySimulator.Backend.Persistence.Models.Professions;

namespace ColonySimulator.Backend.Helpers.Tests;

/// <summary>
/// Seeder to seed test data
/// </summary>

public class TestDataSeeder
{
    /// <summary>
    /// Db context property
    /// </summary>
    public ColonySimulatorContext Context { get; }
    
    /// <summary>
    /// Constructor in it
    /// </summary>
    /// <param name="context"></param>
    public TestDataSeeder(ColonySimulatorContext context)
    {
        Context = context;
    }
    
    /// <summary>
    /// Method to seed test data in db
    /// </summary>
    
    public async Task SeedTestData()
    {
        await Context.Proffesions.AddRangeAsync(TestDataHelper.TestSeederData());
        await Context.SaveChangesAsync();
    }
}