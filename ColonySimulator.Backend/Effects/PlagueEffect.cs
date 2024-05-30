using System.Diagnostics.CodeAnalysis;

namespace ColonySimulator.Backend.Effects;

/// <summary>
/// Effects caused by plagues 
/// </summary>
public class PlagueEffect : Effect
{
    /// <summary>
    /// Empty constructor
    /// </summary>
    public PlagueEffect()
    {
    }
    
    /// <summary>
    /// Setting contructor
    /// </summary>
    /// <param name="name">Name of threat</param>
    /// <param name="damage">Amount of entity damage done</param>
    /// <param name="medicineCount">Required count of medicine</param>
    /// <param name="isSick">status change of entity</param>
    /// <param name="requiredMedicLevel"> Required combined level of medics</param>
    
    [SetsRequiredMembers]
    public PlagueEffect(string name, int damage, int medicineCount, bool isSick, int requiredMedicLevel)
    {
        Name = name;
        Damage = damage;
        MedicineCount = medicineCount;
        IsSick = isSick;
        RequiredMedicLevel = requiredMedicLevel;
    }

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