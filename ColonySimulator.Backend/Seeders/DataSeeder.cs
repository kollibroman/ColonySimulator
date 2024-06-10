using Serilog;
using Spectre.Console;
using ColonySimulator.Backend.Helpers;
using Newtonsoft.Json;

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
    private readonly PopCounter _popCount;
    private readonly Year _year;
    
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
    public DataSeeder(ProfessionSeeder professionSeeder, ResourceSeeder resourceSeeder, ThreatSeeder threatSeeder, ILogger logger, PopCounter counter, Year year)
    {
        _professionSeeder = professionSeeder;
        _resourceSeeder = resourceSeeder;
        _threatSeeder = threatSeeder;
        _logger = logger;
        _popCount = counter;
        _year = year;
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
            //New input seeder validating data when prompting
             _apothecaryCount = AnsiConsole.Prompt(new TextPrompt<int>("Apothecaries: ")
                .ValidationErrorMessage("[red]Inproper input!![/]")
                .Validate(_apothecaryCount =>
                {
                    return _apothecaryCount switch
                    {
                        <= 0 => ValidationResult.Error("[red]You need at least one apothecary[/]"),
                        >= 100 => ValidationResult.Error("[red]Don't exceed 100 inputed apothecaries[/]"),
                        _ => ValidationResult.Success()
                   };
                }));
             //Adding Some better looking input
             _blackSmithCount = AnsiConsole.Prompt(new TextPrompt<int>("Black Smiths: ")
                 .ValidationErrorMessage("[red]Inproper input!![/]")
                 .Validate(_blackSmithCount =>
                 {
                     return _blackSmithCount switch
                     {
                         <= 0 => ValidationResult.Error("[red]You need at least one black smith[/]"),
                         >= 100 => ValidationResult.Error("[red]Don't exceed 100 inputed black smiths[/]"),
                         _ => ValidationResult.Success()
                     };
                 }));
             _farmerCount = AnsiConsole.Prompt(new TextPrompt<int>("Farmers: ")
                 .ValidationErrorMessage("[red]Inproper input!![/]")
                 .Validate(_farmerCount =>
                 {
                     return _farmerCount switch
                     {
                         <= 0 => ValidationResult.Error("[red]You need at least one farmer[/]"),
                         >= 100 => ValidationResult.Error("[red]Don't exceed 100 inputed Farmers[/]"),
                         _ => ValidationResult.Success()
                     };
                 }));
             _timberCount = AnsiConsole.Prompt(new TextPrompt<int>("Timbers: ")
                 .ValidationErrorMessage("[red]Inproper input!![/]")
                 .Validate(_timberCount =>
                 {
                     return _timberCount switch
                     {
                         <= 0 => ValidationResult.Error("[red]You need at least one timber[/]"),
                         >= 100 => ValidationResult.Error("[red]Don't exceed 100 inputed Timbers[/]"),
                         _ => ValidationResult.Success()
                     };
                 }));
             _medicCount = AnsiConsole.Prompt(new TextPrompt<int>("Medics: ")
                 .ValidationErrorMessage("[red]Inproper input!![/]")
                 .Validate(_medicCount =>
                 {
                     return _medicCount switch
                     {
                         <= 0 => ValidationResult.Error("[red]You need at least one medic[/]"),
                         >= 201 => ValidationResult.Error("[red]Don't exceed 100 inputed Medics[/]"),
                         _ => ValidationResult.Success()
                     };
                 }));
            //Changed seeder for trader to create only one
            _traderCount = 1;
            _popCount.ApothecariesCount = _apothecaryCount;
            _popCount.BlackSmithCount = _blackSmithCount;
            _popCount.FarmerCount = _farmerCount;
            _popCount.TimberCount = _timberCount;
            _popCount.MedicCount = _medicCount;
            
            _cropsCount = AnsiConsole.Prompt(new TextPrompt<int>("Crops: ")
                .ValidationErrorMessage("[red]Inproper input!![/]")
                .Validate(_cropsCount =>
                {
                    return _cropsCount switch
                    {
                        < 100 => ValidationResult.Error("[red]You need at least 100 crop[/]"),
                        >= 300 => ValidationResult.Error("[red]Don't exceed 300 inputed Crops[/]"),
                        _ => ValidationResult.Success()
                    };
                }));
            _herbsCount = AnsiConsole.Prompt(new TextPrompt<int>("Herbs: ")
                .ValidationErrorMessage("[red]Inproper input!![/]")
                .Validate(_herbsCount =>
                {
                    return _herbsCount switch
                    {
                        < 1 => ValidationResult.Error("[red]You need at least 10 herbs[/]"),
                        >= 100 => ValidationResult.Error("[red]Don't exceed 100 inputed Herbs[/]"),
                        _ => ValidationResult.Success()
                    };
                }));
            _medicCount = AnsiConsole.Prompt(new TextPrompt<int>("Medicine: ")
                .ValidationErrorMessage("[red]Inproper input!![/]")
                .Validate(_medicCount =>
                {
                    return _medicCount switch
                    {
                        < 5 => ValidationResult.Error("[red]You need at least 5 medicine[/]"),
                        >= 100 => ValidationResult.Error("[red]Don't exceed 100 inputed Medicine[/]"),
                        _ => ValidationResult.Success()
                    };
                }));
            _weaponryCount = AnsiConsole.Prompt(new TextPrompt<int>("Weaponry: ")
                .ValidationErrorMessage("[red]Inproper input!![/]")
                .Validate(_weaponryCount =>
                {
                    return _weaponryCount switch
                    {
                        < 10 => ValidationResult.Error("[red]You need at least 10 weaponry[/]"),
                        >= 100 => ValidationResult.Error("[red]Don't exceed 100 inputed Weaponry[/]"),
                        _ => ValidationResult.Success()
                    };
                }));
            _woodCount = AnsiConsole.Prompt(new TextPrompt<int>("Wood: ")
                .ValidationErrorMessage("[red]Inproper input!![/]")
                .Validate(_woodCount =>
                {
                    return _woodCount switch
                    {
                        < 10 => ValidationResult.Error("[red]You need at least 10 wood[/]"),
                        >= 100 => ValidationResult.Error("[red]Don't exceed 100 inputed Wood[/]"),
                        _ => ValidationResult.Success()
                    };
                }));
        }
        catch (ArgumentNullException ex)
        {
             _logger.Error(ex.Message);
             Console.WriteLine("You can't pass empty input!");
        }
    }
    
    /// <summary>
    /// Loads data from file
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    public async Task LoadDataFromFileAsync(CancellationToken ct)
    {
        var serializedString = await File.ReadAllTextAsync("ExampleData.json", ct);
        var data = JsonConvert.DeserializeObject<InputStorer>(serializedString);

        if (data is not null)
        {
            _year.SimDuration = data.Duration;
            _apothecaryCount = data.Data.ApothecaryCount;
            _blackSmithCount = data.Data.BlackSmithCount;
            _farmerCount = data.Data.FarmerCount;
            _timberCount = data.Data.TimberCount;
            _medicCount = data.Data.MedicCount;
            _traderCount = data.Data.TraderCount;
            _cropsCount = data.Data.CropsCount;
            _herbsCount = data.Data.HerbsCount;
            _medicineCount = data.Data.MedicineCount;
            _weaponryCount = data.Data.WeaponryCount;
            _woodCount = data.Data.WoodCount;
        }
        
        Console.WriteLine(serializedString);
    }
    
    /// <summary>
    /// Seeds data with random amounts of data
    /// </summary>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Completed task</returns>
    public Task SeedRandomData(CancellationToken ct)
    {
        var rnd = new Random();
        
        _year.SimDuration = rnd.Next(10, 300);
        _apothecaryCount = rnd.Next(1, 100);
        _blackSmithCount = rnd.Next(1, 100);
        _farmerCount = rnd.Next(1, 100);
        _timberCount = rnd.Next(1, 100);
        _medicCount = rnd.Next(1, 100);
        _traderCount = 1;
        _cropsCount = rnd.Next(100, 500);
        _herbsCount = rnd.Next(10, 100);
        _medicineCount = rnd.Next(5, 100);
        _weaponryCount = rnd.Next(10, 100);
        _woodCount = rnd.Next(10, 100);
        
        return Task.CompletedTask;
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