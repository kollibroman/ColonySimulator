using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Handlers.ProfessionHandlers;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;
using JetBrains.Annotations;

namespace ColonySimulator.tests.Handlers.ProfessionHandlers;

[TestSubject(typeof(TraderHandler))]
public class TraderHandlerTest
{
    [Fact]
    public async Task TradeTest()
    {
        // Arrange
        var crops = new Crops
        {
            Name = "wheat",
            CropsCount = 10
        };
        
        var wood = new Wood
        {
            Name = "oak",
            WoodCount = 5
        };
        
        var medicine = new Medicine
        {
            Name = "panaceum",
            MedicineCount = 7
        };
        
        var herbs = new Herbs
        {
            Name = "peppermint",
            HerbsCount = 3
        };
        
        var weaponry = new Weaponry
        {
            Name = "weapon",
            WeaponryCount = 8
        };
        
        var handler = new TraderHandler();

        // Act
        await handler.Trade(crops, wood, medicine, herbs, weaponry);

        // Assert
        Assert.Equal(10, crops.CropsCount);
        Assert.Equal(5, wood.WoodCount);
        Assert.Equal(7, medicine.MedicineCount);
        Assert.Equal(3, herbs.HerbsCount);
        Assert.Equal(8, weaponry.WeaponryCount);
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
                },
                new Wood
                {
                    Name = "oak",
                    WoodCount = 5
                },
                new Medicine
                {
                    Name = "panaceum",
                    MedicineCount = 7
                },
                new Weaponry
                {
                    Name = "weapon",
                    WeaponryCount = 8
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
                CropsCount = 5
            },
            new Herbs
            {
                Name = "peppermint",
                HerbsCount = 5
            },
            new Wood
            {
                Name = "oak",
                WoodCount = 5
            },
            new Medicine
            {
                Name = "panaceum",
                MedicineCount = 7
            },
            new Weaponry
            {
                Name = "weapon",
                WeaponryCount = 8
            }
        };
        var handler = new TraderHandler();

        // Act
        await handler.ExperienceThreat(effect, proffesion, resources);

        // Assert
        Assert.Equal(100, proffesion.Vitality);
        Assert.Equal(0, ((Crops)resources[0]).CropsCount);
        Assert.Equal(0, ((Herbs)resources[1]).HerbsCount);
    }
}