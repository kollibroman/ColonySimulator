namespace ColonySimulator.Backend.Persistence.Models.Threats;

public class PlagueThreat : Threat
{
    public int RequiredMedicalLevel { get; set; }
    public int RequiredMedicineCount { get; set; }
}