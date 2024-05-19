using ColonySimulator.Backend.Helpers;
using Microsoft.Extensions.Hosting;
using Serilog;
using ILogger = Serilog.ILogger;

namespace ColonySimulator.Backend.Services;

public class StartupService : IHostedService
{
    private readonly ILogger _logger;
    private readonly Year _year;

    public StartupService(Year year, ILogger logger)
    {
        _year = year;
        _logger = logger;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _year.YearOfSim++;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Bye world");
    }
}