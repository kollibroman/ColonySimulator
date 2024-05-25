using ColonySimulator.Backend.Helpers;
using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Persistence.Models.Professions;
using ColonySimulator.Backend.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using ILogger = Serilog.ILogger;

namespace ColonySimulator.Backend.Services;

public class StartupService : IHostedService
{
    private readonly ILogger _logger;
    private readonly Year _year;
    private readonly IServiceScopeFactory _serviceScope;
    private readonly DataDisplayService _displayService;

    public StartupService(Year year, ILogger logger, IServiceScopeFactory serviceScope,
        DataDisplayService displayService)
    {
        _year = year;
        _logger = logger;
        _serviceScope = serviceScope;
        _displayService = displayService;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        //_year.YearOfSim++;

        using var scope = _serviceScope.CreateScope();
        var dataSeeder = scope.ServiceProvider.GetService<DataSeeder>();
        var dbContext = scope.ServiceProvider.GetService<ColonySimulatorContext>();

        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
        
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

        var threatOverview = new ThreatsOverview
        {
            ThreatsDefeated = await dbContext.PlagueThreats.Where(x => x.Id % 2 == 0).ToListAsync(cancellationToken),
            ThreatsYieldedTo = await dbContext.PlagueThreats.Where(x => x.Id % 2 != 0).ToListAsync(cancellationToken),
        };
        
        Console.WriteLine(_displayService.SerializeAndDisplayData<ProfessionsOverview, ThreatsOverview>(profOverview, threatOverview));
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceScope.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<ColonySimulatorContext>();

        //await dbContext!.Database.EnsureDeletedAsync(cancellationToken);
        Console.WriteLine("Bye world");
    }
}