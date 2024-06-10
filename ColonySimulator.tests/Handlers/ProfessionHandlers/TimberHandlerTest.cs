using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Handlers.ProfessionHandlers;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;
using JetBrains.Annotations;

namespace ColonySimulator.tests.Handlers.ProfessionHandlers;

[TestSubject(typeof(TimberHandler))]
public class TimberHandlerTest
{
    [Fact]
    public async Task CreateWoodTest()
    {
        // Arrange
        var wood = new Wood
        {
            Name = "oak",
            WoodCount = 0
        };
        
        var timLevel = 2;
        var handler = new TimberHandler();

        // Act
        await handler.CreateWood(wood, timLevel);

        // Assert
        Assert.Equal(4, wood.WoodCount);
    }

    [Fact]
    public async Task ExperienceThreatTest_FightingThreatEffect()
    {
        // Arrange
        var effect = new FightingThreatEffect
        {
            Name = "Army",
            Damage = 10, 
            ResourcesStolen =
            [
                new Wood
                {
                    Name = "oak",
                    WoodCount = 5
                }
            ]
        };
        var proffesion = new Proffesion
        {
            Vitality = 100
        };
        var resources = new List<Resource>
        {
            new Wood
            {
                Name = "oak",
                WoodCount = 10
            }
        };
        var handler = new TimberHandler();

        // Act
        await handler.ExperienceThreat(effect, proffesion, resources);

        // Assert
        Assert.Equal(90, proffesion.Vitality);
        Assert.Equal(5, ((Wood)resources[0]).WoodCount);
    }
}