using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Seeders;

/// <summary>
/// Seeder for resources
/// </summary>
public class ResourceSeeder
{
    private readonly ColonySimulatorContext _dbContext;
    
    /// <summary>
    /// Constructor for it with DI parameters
    /// </summary>
    /// <param name="dbContext">Db context</param>
    public ResourceSeeder(ColonySimulatorContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    /// <summary>
    /// Seeder for crops
    /// </summary>
    /// <param name="cropsCount">Number of crops</param>
    /// <param name="ct">Cancellation token</param>
    public async Task SeedCrops(int cropsCount, CancellationToken ct)
    {
        var crop = new Crops
        {
            Name = "wheat",
            CropsCount = cropsCount
        };

        await _dbContext.Crops.AddAsync(crop, ct);
        await _dbContext.SaveChangesAsync(ct);
    }
    
    /// <summary>
    /// Seeder for herbs
    /// </summary>
    /// <param name="herbsCount">Number of herbs</param>
    /// <param name="ct">Cacellation token</param>
    public async Task SeedHerbs(int herbsCount, CancellationToken ct)
    {
        var herb = new Herbs
        {
            Name = "peppermint",
            HerbsCount = herbsCount
        };
        
        await _dbContext.Herbs.AddAsync(herb, ct);
        await _dbContext.SaveChangesAsync(ct);
    }
    
    /// <summary>
    /// Seeder for medicine
    /// </summary>
    /// <param name="medicineCount">Number of medicine</param>
    /// <param name="ct">Cancellation token</param>
    public async Task SeedMedicine(int medicineCount, CancellationToken ct)
    {
        var medicine = new Medicine
        {
            Name = "Panaceum",
            MedicineCount = medicineCount
        };
        
        await _dbContext.Medicines.AddAsync(medicine, ct);
        await _dbContext.SaveChangesAsync(ct);
    }
    
    /// <summary>
    /// Seeder for weaponry
    /// </summary>
    /// <param name="weaponryCount">Number of weaponry</param>
    /// <param name="ct">Cancellation token</param>
    public async Task SeedWeaponry(int weaponryCount, CancellationToken ct)
    {
        var weaponry = new Weaponry
        {
            Name = "Big weapon",
            WeaponryCount = weaponryCount
        };
        
        await _dbContext.Weaponry.AddAsync(weaponry, ct);
        await _dbContext.SaveChangesAsync(ct);
    }
    
    /// <summary>
    /// Seeder for wood
    /// </summary>
    /// <param name="woodCount">Number of wood</param>
    /// <param name="ct">Cancellation token</param>
    public async Task SeedWood(int woodCount, CancellationToken ct)
    {
        var wood = new Wood
        {
            Name = "oak",
            WoodCount = woodCount
        };
        
        await _dbContext.Wood.AddAsync(wood, ct);
        await _dbContext.SaveChangesAsync(ct);
    }
}