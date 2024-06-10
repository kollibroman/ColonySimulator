using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Handlers.ProfessionHandlers;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;
using JetBrains.Annotations;

namespace ColonySimulator.tests.Handlers.ProfessionHandlers;

[TestSubject(typeof(BlackSmithHandler))]
public class BlackSmithHandlerTest
{
    [Fact]
    public async Task CreateWeaponTest()
    {
        // Arrange
        var weapon = new Weaponry
        {
            Name = "sword",
            WeaponryCount = 0
        };
        
        var wood = new Wood
        {
            Name = "oak",
            WoodCount = 10
        };
        
        var blackLevel = 2;
        var handler = new BlackSmithHandler();

        // Act
        await handler.CreateWeapon(weapon, wood, blackLevel);

        // Assert
        Assert.Equal(6, wood.WoodCount);
        Assert.Equal(2, weapon.WeaponryCount);
    }

    [Fact]
    public async Task ExperienceThreatTest()
    {
        // Arrange
        var effect = new FightingThreatEffect
        {
            Name = "Army",
            Damage = 10,
            ResourcesStolen = [
                new Weaponry
                {
                    Name = "sword",
                    WeaponryCount = 5
                }, 
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
            new Weaponry
            {
                Name = "sword",
                WeaponryCount = 10
            }, 
            new Wood
            {
                Name = "oak",
                WoodCount = 10
            }
        };
        var handler = new BlackSmithHandler();

        // Act
        await handler.ExperienceThreat(effect, proffesion, resources);

        // Assert
        Assert.Equal(90, proffesion.Vitality);
        Assert.Equal(5, ((Weaponry)resources[0]).WeaponryCount);
        Assert.Equal(5, ((Wood)resources[1]).WoodCount);
    }
}