using ColonySimulator.Backend.Helpers;
using ColonySimulator.Backend.Helpers.Tests;
using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
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

    public StartupService(ILogger logger, IServiceScopeFactory serviceScope,
        DataDisplayService displayService, StartSimulationService simulationService)
    {
        _logger = logger;
        _serviceScope = serviceScope;
        _displayService = displayService;
        _simulationService = simulationService;
    }
    /// <summary>
    /// application startup method
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        //_year.YearOfSim++;

        using var scope = _serviceScope.CreateScope();
        var dataSeeder = scope.ServiceProvider.GetService<DataSeeder>();
        var dbContext = scope.ServiceProvider.GetService<ColonySimulatorContext>();
        var testSeeder = scope.ServiceProvider.GetService<TestDataSeeder>();
        
        await testSeeder.SeedTestData();

        await dbContext!.Database.EnsureCreatedAsync(cancellationToken);
        
        await dataSeeder!.GetSeedingDataAsync(cancellationToken);
        await dataSeeder.SeedData(cancellationToken);

        var profOverview = new ProfessionsOverview
        {
            Apothecaries = await dbContext.Apothecaries.ToListAsync(cancellationToken),
            BlackSmiths = await dbContext.BlackSmiths.ToListAsync(cancellationToken),
            Timbers = await dbContext.Timbers.ToListAsync(cancellationToken),
            Traders = await dbContext.Traders.ToListAsync(cancellationToken),
            Medics = await dbContext.Medics.ToListAsync(cancellationToken),
            Farmers = await dbContext.Farmers.ToListAsync(cancellationToken),
        };
        
        //change to some reliable values later
        var threatOverview = new ThreatsOverview
        {
            ThreatsDefeated = await dbContext.PlagueThreats.Where(x => x.Id % 2 == 0).ToListAsync(cancellationToken),
            ThreatsYieldedTo = await dbContext.PlagueThreats.Where(x => x.Id % 2 != 0).ToListAsync(cancellationToken),
        };
        
        Console.WriteLine(_displayService.SerializeAndDisplayData<ProfessionsOverview, ThreatsOverview>(profOverview, threatOverview));

        await _simulationService.RunAsync(cancellationToken);
    }
    
    /// <summary>
    /// application shutdown
    /// </summary>
    /// <param name="cancellationToken"></param>
    public async Task StopAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceScope.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<ColonySimulatorContext>();

        //await dbContext!.Database.EnsureDeletedAsync(cancellationToken);
        Console.WriteLine("Bye world");
    }
}