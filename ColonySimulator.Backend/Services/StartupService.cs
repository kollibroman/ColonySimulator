using ColonySimulator.Backend.Handlers.Interfaces.ProfessionsInterfaces;
using ColonySimulator.Backend.Handlers.ProfessionHandlers;
using ColonySimulator.Backend.Persistence.Models.Resources;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ColonySimulator.Backend.Services;

public class StartupService : IHostedService
{
    private ILogger<StartupService> _logger;
    private Year year;

    public StartupService(ILogger<StartupService> logger)
    {
        _logger = logger;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        
        year.YearOfSim++;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Bye world");
    }
}