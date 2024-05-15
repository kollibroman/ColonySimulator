namespace ColonySimulator.Backend.Persistence.Models.Threats;

public class FightingThreat : Threat
{   
    public int RequiredSmithingLevel { get; set; }
    public int RequiredWeaponryCount { get; set; }
}