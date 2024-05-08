using Microsoft.Extensions.Hosting;

namespace ColonySimulator.Backend.Services;

public class StartupService : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Hello World");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Bye world");
        return Task.CompletedTask;
    }
}