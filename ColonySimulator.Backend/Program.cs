using ColonySimulator.Backend.Helpers;
using ColonySimulator.Backend.Services;
using ColonySimulator.Backend.Persistence;
using ColonySimulator.Backend.Seeders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ColonySimulator.Backend;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args);
        builder.ConfigureAppConfiguration(x =>
        {
            x.AddJsonFile("./appsettings.json", optional: true, reloadOnChange: true);
        })
        .ConfigureServices((config, services) =>
        {
            services.AddHostedService<StartupService>();
            var connectionString = config.Configuration.GetConnectionString("BasicDb");

            services.AddScoped<ProfessionSeeder>();
            services.AddScoped<ResourceSeeder>();
            services.AddScoped<ThreatSeeder>();
            
            services.AddSingleton<Year>();
            services.AddSingleton<PopCounter>();
            
            services.AddSqlite<ColonySimulatorContext>(connectionString);
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