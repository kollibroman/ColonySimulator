namespace ColonySimulator.persistence.Models.ModelsThreats;

public class FightningThreat : Threat
{   
    public int RequiredSmithingLevel { get; set; }
    public int RequiredWeaponryCount { get; set; }
}