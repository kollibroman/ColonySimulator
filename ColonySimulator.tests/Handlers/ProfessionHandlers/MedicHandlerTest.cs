using ColonySimulator.Backend.Effects;
using ColonySimulator.Backend.Handlers.ProfessionHandlers;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Persistence.Models.Resources;
using JetBrains.Annotations;

namespace ColonySimulator.tests.Handlers.ProfessionHandlers;

[TestSubject(typeof(MedicHandler))]
public class MedicHandlerTest
{
    [Fact]
    public async Task HealTest()
    {
        // Arrange
        var medicine = new Medicine
        {
            Name = "panaceum",
            MedicineCount = 10
        };
        
        var person = new Farmer()
        {
            Vitality = 20,
            IsSick = true
        };
        
        var medLevel = 2;
        var handler = new MedicHandler();

        // Act
        await handler.Heal(medicine, person, medLevel);

        // Assert
        Assert.False(person.IsSick);
        Assert.Equal(8, medicine.MedicineCount);
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
                new Medicine
                {
                    Name = "panaceum",
                    MedicineCount = 5
                }
            ]
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
            }
        };
        var handler = new MedicHandler();

        // Act
        await handler.ExperienceThreat(effect, proffesion, resources);

        // Assert
        Assert.Equal(90, proffesion.Vitality);
        Assert.Equal(5, ((Medicine)resources[0]).MedicineCount);
    }
}