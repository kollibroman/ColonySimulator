using ColonySimulator.Backend.Helpers;
using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Seeders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Serilog;
using Spectre.Console;
using ILogger = Serilog.ILogger;

namespace ColonySimulator.Backend.Services;

/// <summary>
/// Startup class
/// </summary>
public class StartupService : IHostedService
{
    private readonly ILogger _logger;
    private readonly IServiceScopeFactory _serviceScope;
    private readonly DataDisplayService _displayService;
    private readonly StartSimulationService _simulationService;
    private readonly EndDataStorer _dataStorer;
    private readonly RandomSeedingData _randomSeedingData;

    /// <summary>
    /// Constructor for StartupService
    /// </summary>
    /// <param name="logger">logger</param>
    /// <param name="serviceScope">Scope of services</param>
    /// <param name="displayService">display service</param>
    /// <param name="simulationService">Simulation start service</param>
    /// <param name="dataStorer"></param>
    /// <param name="radomSeedingData"></param>
    public StartupService(ILogger logger, IServiceScopeFactory serviceScope,
        DataDisplayService displayService, StartSimulationService simulationService, EndDataStorer dataStorer,
        RandomSeedingData radomSeedingData)
    {
        _logger = logger;
        _serviceScope = serviceScope;
        _displayService = displayService;
        _simulationService = simulationService;
        _dataStorer = dataStorer;
        _randomSeedingData = radomSeedingData;
    }
    /// <summary>
    /// application startup method
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceScope.CreateScope();
        var dataSeeder = scope.ServiceProvider.GetService<DataSeeder>();
        var dbContext = scope.ServiceProvider.GetService<ColonySimulatorContext>();

        await dbContext!.Database.EnsureCreatedAsync(cancellationToken);
        
        var inputChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[red]Choose seed data method[/]")
                .PageSize(3)
                .AddChoices(["[green]Seed manually[/]", "[blue]Seed data from file[/]", "[yellow]Seed data randomly[/]"]
                ));
        
        bool isManual = false;
        
        switch (inputChoice)
        {
            case "[green]Seed manually[/]":
                AnsiConsole.Markup("[red]Seeding data manually[/]");
                isManual = true;
                await dataSeeder!.GetSeedingDataAsync(cancellationToken);
                break;
            case "[blue]Seed data from file[/]":
                AnsiConsole.Markup("[red]Seeding data from file[/]");
                await dataSeeder!.LoadDataFromFileAsync(cancellationToken);
                break;
            case "[yellow]Seed data randomly[/]":
                AnsiConsole.Markup("[red]Seeding data randomly[/]");
                await dataSeeder!.SeedRandomData(cancellationToken);
                break;
        }

        await dataSeeder.SeedData(cancellationToken);
        await _simulationService.RunAsync(isManual, cancellationToken);
        
    }
    
    /// <summary>
    /// application shutdown
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceScope.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<ColonySimulatorContext>();

        var serializedString = JsonConvert.SerializeObject(_dataStorer, Formatting.Indented);
        await File.WriteAllTextAsync("EndData.json", serializedString, cancellationToken);

        if (_randomSeedingData.Duration != 0)
        {
            var serializedData = JsonConvert.SerializeObject(_randomSeedingData, Formatting.Indented);
            await File.WriteAllTextAsync("RandomStartData.json", serializedData, cancellationToken);
        }

        await dbContext!.Database.EnsureDeletedAsync(cancellationToken);
    }
}