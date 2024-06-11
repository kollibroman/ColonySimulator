namespace ColonySimulator.Backend.Persistence.Models.Threats;

/// <summary>
/// PlagueThreat entity model
/// </summary>
public class PlagueThreat : Threat
{
    /// <summary>
    /// Required level of medical staff
    /// </summary>
    public int RequiredMedicalLevel { get; set; }
    
    /// <summary>
    /// Required medicine count
    /// </summary>
    public int RequiredMedicineCount { get; set; }
}