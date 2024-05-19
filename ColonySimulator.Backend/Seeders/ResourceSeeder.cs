using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Persistence.Models.Resources;

namespace ColonySimulator.Backend.Seeders;

public class ResourceSeeder
{
    private readonly ColonySimulatorContext _dbContext;

    public ResourceSeeder(ColonySimulatorContext dbContext)
    {
        _dbContext = dbContext;
    }

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
    
    public async Task SeedHerbs(int herbsCount, CancellationToken ct)
    {
        var herb = new Herbs
        {
            HerbsCount = herbsCount
        };
        
        await _dbContext.Herbs.AddAsync(herb, ct);
        await _dbContext.SaveChangesAsync(ct);
    }
    
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