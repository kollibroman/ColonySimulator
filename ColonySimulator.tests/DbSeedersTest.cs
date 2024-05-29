using ColonySimulator.Backend.Helpers.Tests;
using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.tests.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.EntityFrameworkCore;

namespace ColonySimulator.tests;

public class DbSeedersTest
{
    private readonly TestDataSeeder _dataSeeder;
    public DbSeedersTest()
    {
        var options = new DbContextOptionsBuilder<ColonySimulatorContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new ColonySimulatorContext(options);

        _dataSeeder = new TestDataSeeder(context);
    }
    
    [Fact]
    public async Task Get_ProfessionsAsync()
    {
        // Arrange
        await _dataSeeder.SeedTestData();

        // Act
        var context = _dataSeeder.Context;
        var professions = await context.Proffesions.ToListAsync();

        // Assert
        Assert.NotNull(professions);
        Assert.NotEmpty(professions);

        var entityList = TestDataHelper.TestSeederData();
        Assert.Equal(entityList.Count, professions.Count);
    }
}