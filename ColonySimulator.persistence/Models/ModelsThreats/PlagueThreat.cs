namespace ColonySimulator.persistence.Models.ModelsThreats;

public class PlagueThreat : Threat
{
    public int RequiredMedicalLevel { get; set; }
    public int RequiredMedicineCount { get; set; }
}