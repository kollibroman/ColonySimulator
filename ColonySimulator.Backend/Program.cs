using Microsoft.Extensions.Hosting;
using Serilog;

namespace ColonySimulator.Backend;

public class Program
{
    public static async Task Main(string[] args)
    {
        IHostBuilder builder = Host.CreateDefaultBuilder(args);
        builder.ConfigureServices(services =>
        {
            
        })
        .ConfigureLogging(x =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .Enrich.FromLogContext()
                .CreateLogger();
        })
        .UseConsoleLifetime()
        .UseSerilog();

        var host = builder.Build();
        await host.RunAsync();
    }
}