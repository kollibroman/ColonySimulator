using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Persistence.Enums;
using ColonySimulator.Backend.Persistence.Models.Professions;

namespace ColonySimulator.Backend.Helpers.Tests;

/// <summary>
/// Class with sample data to match in tests
/// </summary>

public static class TestDataHelper 
{
    /// <summary>
    /// Method to test sample data from db
    /// </summary>
    /// <returns>List of professions</returns>
    public static List<Proffesion> TestSeederData()
    {
        return
        [
            new Apothecary
            {
                Agility = 1,
                ApothecaryLevel = 1,
                Gender = Gender.Man,
                RequiredAgility = 1,
                RequiredStrength = 1,
                Strength = 1,
                Vitality = 3,
                ResourceConsumption = 4.0,
                IsSick = false
            },

            new BlackSmith
            {
                Agility = 2,
                BlackSmithLevel = 1,
                Gender = Gender.Man,
                RequiredAgility = 2,
                RequiredStrength = 4,
                Strength = 1,
                Vitality = 3,
                ResourceConsumption = 4.0,
                IsSick = false
            },

            new Farmer
            {
                Agility = 3,
                FarmingLevel = 1,
                Gender = Gender.Man,
                RequiredAgility = 3,
                RequiredStrength = 5,
                Strength = 5,
                Vitality = 3,
                ResourceConsumption = 4.0,
                IsSick = false
            },

            new Medic
            {
                Agility = 2,
                MedicLevel = 1,
                Gender = Gender.Man,
                RequiredAgility = 2,
                RequiredStrength = 1,
                Strength = 1,
                Vitality = 3,
                ResourceConsumption = 4.0,
                IsSick = false
            },

            new Timber
            {
                Agility = 4,
                TimberLevel = 1,
                Gender = Gender.Man,
                RequiredAgility = 4,
                RequiredStrength = 5,
                Strength = 5,
                Vitality = 3,
                ResourceConsumption = 4.0,
                IsSick = false
            },

            new Trader
            {
                Agility = 5,
                TradingLevel = 1,
                Gender = Gender.Man,
                RequiredAgility = 5,
                RequiredStrength = 1,
                Strength = 1,
                Vitality = 3,
                ResourceConsumption = 4.0,
                IsSick = false
            }
        ];
    }
}