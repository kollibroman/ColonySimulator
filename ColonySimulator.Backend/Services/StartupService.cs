using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ColonySimulator.Backend.Services;

public class StartupService : IHostedService
{
    private ILogger<StartupService> _logger;

    public StartupService(ILogger<StartupService> logger)
    {
        _logger = logger;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("twoja stara");
        Console.WriteLine("Hello World");
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Bye world");
    }
}