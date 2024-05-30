namespace ColonySimulator.Backend.Effects;

/// <summary>
/// Effects caused by plagues 
/// </summary>
public class PlagueEffect : Effect
{
    /// <summary>
    /// bool changing IsSick status of entity
    /// </summary>
    public required bool IsSick { get; init; } = true;
    /// <summary>
    /// Required medicine cout
    /// </summary>
    public required int MedicineCount { get; init; }
    
    /// <summary>
    /// Required level of medic to treat it
    /// </summary>
    public required int RequiredMedicLevel { get; init; }
}