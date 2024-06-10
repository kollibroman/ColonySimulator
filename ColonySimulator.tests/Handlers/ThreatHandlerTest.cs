using System.Linq.Expressions;
using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Handlers;
using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;
using ColonySimulator.Backend.Persistence.Models.Threats;
using ColonySimulator.tests.Extensions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ColonySimulator.tests.Handlers;

[TestSubject(typeof(ThreatHandler))]
public class ThreatHandlerTest
{
    private readonly Mock<ColonySimulatorContext> _mockDbContext;
    private readonly ThreatHandler _threatHandler;

    
    private DbContextOptions<ColonySimulatorContext> CreateNewContextOptions()
    {
        return new DbContextOptionsBuilder<ColonySimulatorContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }
    
    public ThreatHandlerTest()
    {
        _mockDbContext = new Mock<ColonySimulatorContext>(CreateNewContextOptions());
        _threatHandler = new ThreatHandler(_mockDbContext.Object);
    }

    [Fact]
    public async Task CalculateAffectionTest()
    {
        // Arrange
        var medic = new Medic
        {
            MedicLevel = 5,
            Agility = 3,
            Strength = 4
        };
        var threat = new PlagueThreat
        {
            Name = "Ligma",
            ThreatLevel = 2
        };

        // Act
        await _threatHandler.CalculateAffection(medic, threat);

        // Assert
        Assert.Equal(0, GetPrivateField<int>(_threatHandler, "_damage"));
    }

    [Fact]
    public void CalculateUsedResourcesTest()
    {
        // Arrange
        var resources = new List<Resource>
        {
            new Crops
            {
                Name = "Wheat", 
                CropsCount = 10
            },
            new Wood
            {
                Name = "Oak", 
                WoodCount = 5
            }
        };
        var threat = new NaturalThreat
        {
            Name = "RandomThreatName",
            ThreatLevel = 4
        };

        // Act
        var result = _threatHandler.CalculateUsedResources(resources, threat);

        // Assert
        Assert.Collection(result,
            item =>
            {
                var crops = Assert.IsType<Crops>(item);
                Assert.Equal("Wheat", crops.Name);
                Assert.Equal(3, crops.CropsCount);
            },
            item =>
            {
                var wood = Assert.IsType<Wood>(item);
                Assert.Equal("Oak", wood.Name);
                Assert.Equal(3, wood.WoodCount);
            });
    }

    [Fact]
    public async Task GenerateEffectsTest()
    {
        // Arrange
        var threat = new PlagueThreat
        {
            Name = "Plague",
            ThreatLevel = 3, 
            RequiredMedicineCount = 5, 
            RequiredMedicalLevel = 2
        };
        var resources = new List<Resource>
        {
            new Medicine
            {
                Name = "Antibiotics", 
                MedicineCount = 10
            }
        };

        // Act
        var result = await _threatHandler.GenerateEffects(threat, resources);

        // Assert
        var effect = Assert.IsType<PlagueEffect>(result);
        Assert.Equal("Plague", effect.Name);
        Assert.True(effect.IsSick);
        Assert.Equal(2, effect.RequiredMedicLevel);
    }

   
    private static T GetPrivateField<T>(object obj, string fieldName)
    {
        var field = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        return (T)field.GetValue(obj);
    }
}