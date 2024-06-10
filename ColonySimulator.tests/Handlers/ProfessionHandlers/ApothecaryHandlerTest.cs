using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Handlers.ProfessionHandlers;
using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;
using ColonySimulator.tests.TestAsyncQuery;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ColonySimulator.tests.Handlers.ProfessionHandlers;

[TestSubject(typeof(ApothecaryHandler))]
public class ApothecaryHandlerTest
{
    [Fact]
    public async Task CreateMedicineTest()
    {
        // Arrange
        var herbs = new Herbs
        {
            Name = "peppermint",
            HerbsCount = 10
        };
        
        var medicine = new Medicine
        {
            Name = "panaceum",
            MedicineCount = 0
        };
        
        var apoLevel = 2;
        var handler = new ApothecaryHandler();

        // Act
        await handler.CreateMedicine(herbs, medicine, apoLevel);

        // Assert
        Assert.Equal(4, herbs.HerbsCount);
        Assert.Equal(2, medicine.MedicineCount);
    }

    [Fact]
    public async Task CollectingHerbsTest()
    {
        // Arrange
        var herbs = new Herbs
        {
            Name = "peppermint",
            HerbsCount = 10
        };
        
        var apoLevel = 2;
        var handler = new ApothecaryHandler();

        // Act
        await handler.CollectingHerbs(herbs, apoLevel);

        // Assert
        Assert.Equal(14, herbs.HerbsCount);
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
                new Medicine
                {
                    Name = "panaceum",
                    MedicineCount = 5
                }, 
                new Herbs
                {
                    Name = "peppermint",
                    HerbsCount = 5
                }]
        };
        
        var proffesion = new Proffesion
        {
            Vitality = 100
        };
        
        var resources = new List<Resource>
        {
            new Medicine
            {
                Name = "panaceum",
                MedicineCount = 10
            }, 
            new Herbs
            {
                Name = "peppermint",
                HerbsCount = 10
            }
        };
        var handler = new ApothecaryHandler();

        // Act
        await handler.ExperienceThreat(effect, proffesion, resources);

        // Assert
        Assert.Equal(90, proffesion.Vitality);
        Assert.Equal(5, ((Medicine)resources[0]).MedicineCount);
        Assert.Equal(5, ((Herbs)resources[1]).HerbsCount);
    }
}