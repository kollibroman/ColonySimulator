using Serilog;

namespace ColonySimulator.Backend.Seeders;

/// <summary>
/// Data seeder class, with combined other seeders
/// </summary>

public class DataSeeder
{
    private readonly ProfessionSeeder _professionSeeder;
    private readonly ResourceSeeder _resourceSeeder;
    private readonly ThreatSeeder _threatSeeder;
    private readonly ILogger _logger;
    
    //Profession config
    private int _apothecaryCount;
    private int _blackSmithCount;
    private int _farmerCount;
    private int _timberCount;
    private int _medicCount;
    private int _traderCount;
    
    //ResourceConfig
    private int _cropsCount;
    private int _herbsCount;
    private int _medicineCount;
    private int _weaponryCount;
    private int _woodCount;
    
    /// <summary>
    /// Constructor for it
    /// </summary>
    /// <param name="professionSeeder">Profession seeder class</param>
    /// <param name="resourceSeeder">Resource seeder class</param>
    /// <param name="threatSeeder">Threat seeder class</param>
    /// <param name="logger">logger class to log data</param>
    public DataSeeder(ProfessionSeeder professionSeeder, ResourceSeeder resourceSeeder, ThreatSeeder threatSeeder, ILogger logger)
    {
        _professionSeeder = professionSeeder;
        _resourceSeeder = resourceSeeder;
        _threatSeeder = threatSeeder;
        _logger = logger;
    }
    
    /// <summary>
    /// Gets seeding data
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    
    public async Task GetSeedingDataAsync(CancellationToken ct)
    {
        Console.WriteLine("Specify number of professions and resources");

        try
        {
            Console.Write("Apothecaries: ");
            _apothecaryCount = int.Parse(Console.ReadLine()!);
            Console.Write("Blacksmiths: ");
            _blackSmithCount = int.Parse(Console.ReadLine()!);
            Console.Write("Farmers: ");
            _farmerCount = int.Parse(Console.ReadLine()!);
            Console.Write("Timbers: ");
            _timberCount = int.Parse(Console.ReadLine()!);
            Console.Write("Medics: ");
            _medicCount = int.Parse(Console.ReadLine()!);
            //Changed seeder for trader to create only one
            _traderCount = 1;
            
            Console.Write("Crops: ");
            _cropsCount = int.Parse(Console.ReadLine()!);
            Console.Write("Herbs: ");
            _herbsCount = int.Parse(Console.ReadLine()!);
            Console.Write("Medicine: ");
            _medicineCount = int.Parse(Console.ReadLine()!);
            Console.Write("Weaponry: ");
            _weaponryCount = int.Parse(Console.ReadLine()!);
            Console.Write("Wood: ");
            _woodCount = int.Parse(Console.ReadLine()!);
        }
        catch (ArgumentNullException ex)
        {
             _logger.Error(ex.Message);
             Console.WriteLine("You can't pass empty input!");
        }
    }

    /// <summary>
    /// Seeds data
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    
    public async Task SeedData(CancellationToken ct)
    {
        //seed professions
        await _professionSeeder.SeedApothecary(_apothecaryCount, ct);
        await _professionSeeder.SeedBlacksmith(_blackSmithCount, ct);
        await _professionSeeder.SeedFarmer(_farmerCount, ct);
        await _professionSeeder.SeedTimbers(_timberCount, ct);
        await _professionSeeder.SeedMedic(_medicCount, ct);
        await _professionSeeder.SeedTraders(_traderCount, ct);
        
        //seed resources
        await _resourceSeeder.SeedCrops(_cropsCount, ct);
        await _resourceSeeder.SeedHerbs(_herbsCount, ct);
        await _resourceSeeder.SeedMedicine(_medicineCount, ct);
        await _resourceSeeder.SeedWeaponry(_weaponryCount, ct);
        await _resourceSeeder.SeedWood(_woodCount, ct);
        
        //seed threats
        await _threatSeeder.SeedFighting(ct);
        await _threatSeeder.SeedNatural(ct);
        await _threatSeeder.SeedPlagues(ct);
    }
}