using ColonySimulator.Backend.Helpers.Tests;
using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Persistence.Models.Professions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.EntityFrameworkCore;

namespace ColonySimulator.tests;

public class DbSeedersTest
{
    private IServiceProvider ServicesProvider { get; set; }
    protected ColonySimulatorContext Context { get; set; }
    
    [SetUp]
    public void SetupBeforeEachTest()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        ServicesProvider = services.BuildServiceProvider();

        Context = ServicesProvider.GetService<ColonySimulatorContext>();
    }

    [Fact]
    public async Task GetProfessionSeederData()
    {
        var colonyContextMock = new Mock<ColonySimulatorContext>();

        colonyContextMock.Setup<DbSet<Proffesion>>(x => x.Proffesions)
            .ReturnsDbSet(TestDataHelper.TestSeederData());

        var data = Context.Proffesions.ToList();
        
        Assert.NotNull(data);
        Assert.Equals(TestDataHelper.TestSeederData(), data);
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<ColonySimulatorContext>();
    }
}