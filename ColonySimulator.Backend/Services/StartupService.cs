using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Seeders;
using ColonySimulator.Backend.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
    private readonly IEndDataWriter _dataWriter;
    private readonly StartSimulationService _simulationService;

    /// <summary>
    /// Constructor for StartupService
    /// </summary>
    /// <param name="logger">logger</param>
    /// <param name="serviceScope">Scope of services</param>
    /// <param name="dataWriter">Writer of end data</param>
    /// <param name="simulationService">Simulation start service</param>
    public StartupService(ILogger logger, IServiceScopeFactory serviceScope,
        IEndDataWriter dataWriter, StartSimulationService simulationService)
    {
        _logger = logger;
        _serviceScope = serviceScope;
        _dataWriter = dataWriter;
        _simulationService = simulationService;
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

        await _dataWriter.WriteEndDataAsync(cancellationToken);

        await dbContext!.Database.EnsureDeletedAsync(cancellationToken);
    }
}