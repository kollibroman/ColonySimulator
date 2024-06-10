using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Handlers.ProfessionHandlers;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.tests.Handlers.ProfessionHandlers;

public class FarmerHandlerTest
{
    [Fact]
    public async Task FarmTest()
    {
        // Arrange
        var crops = new Crops
        {
            Name = "wheat",
            CropsCount = 0
        };
        
        var herbs = new Herbs
        {
            Name = "peppermint",
            HerbsCount = 0
        };
        
        var farmLevel = 2;
        var handler = new FarmerHandler();

        // Act
        await handler.Farm(crops, herbs, farmLevel);

        // Assert
        Assert.Equal(4, crops.CropsCount);
        Assert.Equal(1, herbs.HerbsCount);
    }

    [Fact]
    public async Task ExperienceThreatTest()
    {
        // Arrange
        var effect = new FightingThreatEffect
        {
            Name = "Army",
            Damage = 10, 
            ResourcesStolen =
            [
                new Crops
                {
                    Name = "wheat",
                    CropsCount = 5
                },

                new Herbs
                {
                    Name = "peppermint",
                    HerbsCount = 5
                }
            ]
        };
        var proffesion = new Proffesion
        {
            Vitality = 100
        };
        
        var resources = new List<Resource> 
        {
            new Crops
            {
                Name = "wheat",
                CropsCount = 10
            }, 
            new Herbs
            {
                Name = "peppermint",
                HerbsCount = 10
            } 
        };
        
        var handler = new FarmerHandler();

        // Act
        await handler.ExperienceThreat(effect, proffesion, resources);

        // Assert
        Assert.Equal(90, proffesion.Vitality);
        Assert.Equal(5, ((Crops)resources[0]).CropsCount);
        Assert.Equal(5, ((Herbs)resources[1]).HerbsCount);
    }
}