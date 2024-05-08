using ColonySimulator.Backend.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ColonySimulator.Backend;

public class Program
{
    public static async Task Main(string[] args)
    {
        IHostBuilder builder = Host.CreateDefaultBuilder(args);
        builder.ConfigureAppConfiguration(x =>
        {
            x.AddJsonFile("./appsettings.json", optional: true, reloadOnChange: true);
        })
        .ConfigureServices(services =>
        {
            services.AddHostedService<StartupService>();
        })
        .ConfigureLogging(x =>
        {
            x.AddSerilog();
        })
        .UseConsoleLifetime()
        .UseSerilog((builderContext, configuration) =>
            configuration.ReadFrom.Configuration(builderContext.Configuration));

        var host = builder.Build();
        await host.RunAsync();
    }
}