using System.Linq.Expressions;
using ColonySimulator.Backend.Handlers.ResourceHandlers;
using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;
using ColonySimulator.tests.TestAsyncQuery;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ColonySimulator.tests.Handlers.ResourceHandlers;

[TestSubject(typeof(ResourceHandler))]
public class ResourceHandlerTest
{
    private DbContextOptions<ColonySimulatorContext> CreateNewContextOptions()
    {
        return new DbContextOptionsBuilder<ColonySimulatorContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }
    
    [Fact]
    public async Task ConsumeResourcesTest()
    {
        // Arrange
        var mockContext = new Mock<ColonySimulatorContext>(CreateNewContextOptions());
        var mockDbSetCrops = new Mock<DbSet<Crops>>();
        var mockDbSetHerbs = new Mock<DbSet<Herbs>>();
        var mockDbSetWood = new Mock<DbSet<Wood>>();
        var mockDbSetFarmers = new Mock<DbSet<Farmer>>();
        var mockDbSetApothecaries = new Mock<DbSet<Apothecary>>();
        var mockDbSetMedics = new Mock<DbSet<Medic>>();
        var mockDbSetTimbers = new Mock<DbSet<Timber>>();
        var mockDbSetTraders = new Mock<DbSet<Trader>>();
        var mockDbSetBlackSmiths = new Mock<DbSet<BlackSmith>>();

        var dataCrops = new List<Crops> 
            {
                new Crops
                {
                    Id = 1, 
                    Name = "wheat",
                    CropsCount = 100
                } }.AsQueryable();
        
        var dataHerbs = new List<Herbs>
        {
            new Herbs
            {
                Id = 1, 
                Name = "peppermint",
                HerbsCount = 100
            }
        }.AsQueryable();
        var dataWood = new List<Wood>
        {
            new Wood
            {
                Id = 1, 
                Name = "oak",
                WoodCount = 100
            }
        }.AsQueryable();
        
        var dataFarmers = new List<Farmer>
        {
            new Farmer
            {
                ResourceConsumption = 10
            }
        }.AsQueryable();
        
        var dataApothecaries = new List<Apothecary>
        {
            new Apothecary
            {
                ResourceConsumption = 10
            }
        }.AsQueryable();
        
        var dataMedics = new List<Medic>
        {
            new Medic
            {
                ResourceConsumption = 10
            }
        }.AsQueryable();
        
        var dataTimbers = new List<Timber>
        {
            new Timber
            {
                ResourceConsumption = 10
            }
        }.AsQueryable();
        
        var dataTraders = new List<Trader>
        {
            new Trader
            {
                ResourceConsumption = 10
            }
        }.AsQueryable();
        
        var dataBlackSmiths = new List<BlackSmith>
        {
            new BlackSmith
            {
                ResourceConsumption = 10
            }
        }.AsQueryable();

        SetupMockDbSet(mockDbSetCrops, dataCrops);
        SetupMockDbSet(mockDbSetHerbs, dataHerbs);
        SetupMockDbSet(mockDbSetWood, dataWood);
        SetupMockDbSet(mockDbSetFarmers, dataFarmers);
        SetupMockDbSet(mockDbSetApothecaries, dataApothecaries);
        SetupMockDbSet(mockDbSetMedics, dataMedics);
        SetupMockDbSet(mockDbSetTimbers, dataTimbers);
        SetupMockDbSet(mockDbSetTraders, dataTraders);
        SetupMockDbSet(mockDbSetBlackSmiths, dataBlackSmiths);

        mockContext.Setup(m => m.Crops).Returns(mockDbSetCrops.Object);
        mockContext.Setup(m => m.Herbs).Returns(mockDbSetHerbs.Object);
        mockContext.Setup(m => m.Wood).Returns(mockDbSetWood.Object);
        mockContext.Setup(m => m.Farmers).Returns(mockDbSetFarmers.Object);
        mockContext.Setup(m => m.Apothecaries).Returns(mockDbSetApothecaries.Object);
        mockContext.Setup(m => m.Medics).Returns(mockDbSetMedics.Object);
        mockContext.Setup(m => m.Timbers).Returns(mockDbSetTimbers.Object);
        mockContext.Setup(m => m.Traders).Returns(mockDbSetTraders.Object);
        mockContext.Setup(m => m.BlackSmiths).Returns(mockDbSetBlackSmiths.Object);

        var resourceHandler = new ResourceHandler(mockContext.Object);

        // Act
        await resourceHandler.ConsumeResources(1);

        // Assert
        Assert.Equal(88, dataCrops.First().CropsCount);
    }

    private static void SetupMockDbSet<T>(Mock<DbSet<T>> mockDbSet, IQueryable<T> data) where T : class
    {
        mockDbSet.As<IAsyncEnumerable<T>>().Setup(d => d.GetAsyncEnumerator(default))
            .Returns(new TestAsyncEnumerator<T>(data.GetEnumerator()));
        mockDbSet.As<IQueryable<T>>().Setup(m => m.Provider)
            .Returns(new TestAsyncQueryProvider<T>(data.Provider));
        mockDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
        mockDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
    }
}