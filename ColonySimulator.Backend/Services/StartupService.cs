using ColonySimulator.Backend.Helpers;
using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Seeders;
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

    public StartupService(Year year, ILogger logger, IServiceScopeFactory serviceScope)
    {
        _year = year;
        _logger = logger;
        _serviceScope = serviceScope;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        //_year.YearOfSim++;

        using var scope = _serviceScope.CreateScope();
        var dataSeeder = scope.ServiceProvider.GetService<DataSeeder>();
        
        await dataSeeder!.GetSeedingDataAsync(cancellationToken);
        await dataSeeder.SeedData(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Bye world");
    }
}